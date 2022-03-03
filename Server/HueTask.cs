using GrinHome.Data;
using Microsoft.EntityFrameworkCore;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace GrinHome.Server
{
    public class HueTask : BackgroundService
    {

        public ApplicationDbContext? Db
        {
            get
            {
                try
                {
                    return ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
                }
                catch (Exception ex)   // If there is an issue, takes the last available
                {
                    Serilog.Log.Error(ex, "Get ApplicationDbContext");
                }
                return null;
            }
        }

        private IServiceProvider ServiceProvider { get; }


        public HueTask(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBridgeLocator locator = new HttpBridgeLocator(); //Or: LocalNetworkScanBridgeLocator, MdnsBridgeLocator, MUdpBasedBridgeLocator
            var bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

            //Advanced Bridge Discovery options:
            bridges = await HueBridgeDiscovery.CompleteDiscoveryAsync(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30));
            bridges = await HueBridgeDiscovery.FastDiscoveryWithNetworkScanFallbackAsync(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30));
            bridges = await HueBridgeDiscovery.CompleteDiscoveryAsync(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30));


            await Task.Delay(1);
        }
    }
}
