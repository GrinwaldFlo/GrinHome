using GrinHome.Data;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GrinHome.Server
{
    public class Vps : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }
        private readonly DataService dataService;
        private readonly string path;

        //public ApplicationDbContext? Db
        //{
        //    get
        //    {
        //        try
        //        {
        //            return ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
        //        }
        //        catch (Exception ex)   // If there is an issue, takes the last available
        //        {
        //            Serilog.Log.Error(ex, "Get ApplicationDbContext");
        //        }
        //        return null;
        //    }
        //}
        private ApplicationDbContext? db;

        public Vps(IServiceProvider serviceProvider, DataService dataService, IConfiguration configuration)
        {
            ServiceProvider = serviceProvider;
            this.dataService = dataService;
            path = configuration["MailLog"];
            if (!Directory.Exists(path))
                Log.Error($"VPS: path {path} doesn't existst");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            db = ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
            if (db == null)
            {
                Log.Error("VPS: Failed to link to DB");
                return;
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                CheckMailLogs();
                await Task.Delay(10000, stoppingToken);
            }
        }

        private void CheckMailLogs()
        {
            DirectoryInfo directory = new(path);

            if (!directory.Exists)
                return;

            try
            {
                var files = directory.GetFiles("mail_*.log");

                foreach (var file in files)
                {
                    CheckMailLog(file);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "VPS Mail log");
            }
        }

        private void CheckMailLog(FileInfo file)
        {
            try
            {
                if (db == null)
                    return;
                var regDate = Regex.Match(file.Name, @"^mail_(\d{4})_(\d{2})_(\d{2})\.log$");
                if (!regDate.Success)
                {
                    Log.Warning($"CheckMailLog, wrong pattern for {file.Name}");
                    return;
                }
                DateTime date = new(int.Parse(regDate.Groups[1].Value), int.Parse(regDate.Groups[2].Value), int.Parse(regDate.Groups[3].Value));


                var data = SplitBySection(File.ReadAllText(file.FullName));

                var postfix = db.Postfixes.Where(x => x.DatePostfix == date).FirstOrDefault();
                if (postfix == null)
                    postfix = new Postfix() { DatePostfix = date };
                if (data.TryGetValue("Grand Totals", out string? grandTotals))
                {
                    postfix.Received = FindIntValue("received", grandTotals);
                    postfix.Delivered = FindIntValue("delivered", grandTotals);
                    postfix.Forwarded = FindIntValue("forwarded", grandTotals);
                    postfix.Deferred = FindIntValue("deferred", grandTotals);
                    postfix.Bounced = FindIntValue("bounced", grandTotals);
                    postfix.Rejected = FindIntValue("rejected ", grandTotals);
                    postfix.RejectedWarning = FindIntValue("reject warnings", grandTotals);
                    postfix.Held = FindIntValue("held", grandTotals);
                    postfix.Discarded = FindIntValue("discarded", grandTotals);
                    postfix.BytesReceived = FindIntValue("bytes received", grandTotals);
                    postfix.BytesDelivered = FindIntValue("bytes delivered", grandTotals);
                    postfix.Senders = FindIntValue("senders", grandTotals);
                    postfix.SendingHostDomain = FindIntValue("sending hosts", grandTotals);
                    postfix.Recipients = FindIntValue("recipients", grandTotals);
                    postfix.RecipientHostDomain = FindIntValue("recipient hosts", grandTotals);
                }

                postfix.TopMessageDelivery = data.GetValueOrDefault("Host/Domain Summary: Message Delivery");
                postfix.TopMessageReceived = data.GetValueOrDefault("Host/Domain Summary: Messages Received");
                postfix.TopSendersCount = data.GetValueOrDefault("Senders by message count");
                postfix.TopRecipientsCount = data.GetValueOrDefault("Recipients by message count");
                postfix.TopSendersSize = data.GetValueOrDefault("Senders by message size");
                postfix.TopRecipientsSize = data.GetValueOrDefault("Recipients by message size");
                postfix.MessageRejectDetail = data.GetValueOrDefault("message reject detail");

                if (data.TryGetValue("Warnings", out string? warnings))
                {
                    postfix.SmtpdWarnings = RegexIntValue(@"\ssmtpd \(total:\s(\d+)\)", warnings);
                }

                if(postfix.Id == 0)
                    db.Postfixes.Add(postfix);

                db.SaveChanges();
                file.Delete();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "VPS Mail log");
            }
        }

        private static int FindIntValue(string label, string raw)
        {
            var reg = Regex.Match(raw, $@"\s+(\d+)\s+{label}");
            if (reg.Success)
                return int.Parse(reg.Groups[1].Value);

            reg = Regex.Match(raw, $@"\s+(\d+)k\s+{label}");
            if (reg.Success)
                return int.Parse(reg.Groups[1].Value) * 1024;

            return -1;
        }

        private static int RegexIntValue(string regex, string raw)
        {
            var reg = Regex.Match(raw, regex);
            if (reg.Success)
                return int.Parse(reg.Groups[1].Value);
            return -1;
        }

        private static Dictionary<string, string> SplitBySection(string data)
        {
            string[] datas = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> result = new();

            string curTitle = "";
            StringBuilder curData = new();

            for (int i = 0; i < datas.Length - 1; i++)
            {
                if (Regex.IsMatch(datas[i + 1], @"^-+$"))
                {
                    if (!string.IsNullOrEmpty(curTitle))
                        result.Add(curTitle, curData.ToString());

                    curTitle = datas[i].Trim();
                    curData.Clear();
                }
                else if (!Regex.IsMatch(datas[i], @"^-+$") && !string.IsNullOrEmpty(curTitle))
                {
                    curData.AppendLine(datas[i]);
                }
            }
            if (!string.IsNullOrEmpty(curTitle))
                result.Add(curTitle, curData.ToString());

            return result;
        }
    }
}
