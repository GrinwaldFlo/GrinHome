using GrinHome.Data;
using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace GrinHome.Pages.Home
{
    public partial class PostfixView : ComponentBase
    {
        [Inject] DataService? DataService { get; set; }

        private string[]? DeviceList = Array.Empty<string>();
        private int TimeSpanDays = 30;

        private DataItem[] DataReceived = Array.Empty<DataItem>();
        private DataItem[] DataDelivered = Array.Empty<DataItem>();
        private DataItem[] DataForwarded = Array.Empty<DataItem>();
        private DataItem[] DataDeferred = Array.Empty<DataItem>();
        private DataItem[] DataBounced = Array.Empty<DataItem>();
        private DataItem[] DataRejected = Array.Empty<DataItem>();
        private DataItem[] DataRejectedWarning = Array.Empty<DataItem>();
        private DataItem[] DataHeld = Array.Empty<DataItem>();
        private DataItem[] DataDiscarded = Array.Empty<DataItem>();
        private DataItem[] DataBytesReceived = Array.Empty<DataItem>();
        private DataItem[] DataBytesDelivered = Array.Empty<DataItem>();
        private DataItem[] DataSenders = Array.Empty<DataItem>();
        private DataItem[] DataSendingHostDomain = Array.Empty<DataItem>();
        private DataItem[] DataRecipients = Array.Empty<DataItem>();
        private DataItem[] DataRecipientHostDomain = Array.Empty<DataItem>();
        private DataItem[] DataSmtpdWarnings = Array.Empty<DataItem>();


        protected override async Task OnParametersSetAsync()
        {
            if (DataService == null)
                return;

            DeviceList = DataService.Db?.Sensors.Select(x => x.Name).ToArray();

            if (DeviceList == null || !DeviceList.Any())
                return;

            await UpdateTrend();

            await base.OnParametersSetAsync();
        }

        private async void SetTimeSpanDays(int days)
        {
            TimeSpanDays = days;
            await UpdateTrend();
        }


        private async Task UpdateTrend()
        {
            if (DataService == null || DataService.Db == null)
                return;

            var dataGraphs = await DataService.Db.Postfixes.
                Where(x => x.DatePostfix > DateTime.Now.AddDays(-TimeSpanDays))
                .Select(x => new {x.DatePostfix, x.Received, x.Delivered, x.Forwarded, x.Deferred, x.Bounced, x.Rejected, x.RejectedWarning,
                    x.Held, x.Discarded, BytesReceived = x.BytesReceived / (1024.0f * 1024), BytesDelivered = x.BytesDelivered / (1024.0f * 1024), 
                    x.Senders, x.SendingHostDomain, x.Recipients, x.RecipientHostDomain, x.SmtpdWarnings})
                .AsSplitQuery().AsNoTracking().ToArrayAsync();

            DataReceived = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Received }).ToArray();
            DataDelivered = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Delivered }).ToArray();
            DataForwarded = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Forwarded }).ToArray();
            DataDeferred = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Deferred }).ToArray();
            DataBounced = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Bounced }).ToArray();
            DataRejected = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Rejected }).ToArray();
            DataRejectedWarning = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.RejectedWarning }).ToArray();
            DataHeld = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Held }).ToArray();
            DataDiscarded = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Discarded }).ToArray();
            DataBytesReceived = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.BytesReceived }).ToArray();
            DataBytesDelivered = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.BytesDelivered }).ToArray();
            DataSenders = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Senders }).ToArray();
            DataSendingHostDomain = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.SendingHostDomain }).ToArray();
            DataRecipients = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.Recipients }).ToArray();
            DataRecipientHostDomain = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.RecipientHostDomain }).ToArray();
            DataSmtpdWarnings = dataGraphs.Select(x => new DataItem() { Date = x.DatePostfix, Value = x.SmtpdWarnings }).ToArray();
            await InvokeAsync(StateHasChanged);
        }
    }
}
