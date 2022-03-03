using GrinHome.Areas.Identity;
using GrinHome.Data;
using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetSection("LogPath").Value,
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();



if(!Crypto.SetKey(builder.Configuration["CryptoKey"]))
{
    Crypto.CreateKey();
}


// Database managements
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var version = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContextFactory<ApplicationDbContext>(
          dbContextOptions => dbContextOptions
              .UseMySql(connectionString, version)
#if DEBUG
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
                    );

// Setting up services
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<DataService>();
builder.Services.AddHostedService<GrinHome.Server.MqttTask>();
builder.Services.AddHostedService<GrinHome.Server.HueTask>();
builder.Services.AddHostedService<GrinHome.Server.Vps>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<EmailSender>();
builder.Services.Configure<SmtpSenderOptions>(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
