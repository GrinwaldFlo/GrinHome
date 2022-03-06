using GrinHome.Data.Services;
using GrinHome.Server;
using Microsoft.AspNetCore.Components;
using Serilog;
using System.Reactive.Linq;

namespace GrinHome.Pages.Home
{
    public partial class Index : ComponentBase, IDisposable 
    {
        [Inject] DataService? DataService { get; set; }
        private int testValue;

        private IDisposable? TestStoC;
        private IDisposable? NotificationUpdate;

        private Data.Models.ValueShown[] ValueShowns { get; set; } = Array.Empty<Data.Models.ValueShown>();
        protected override async Task OnInitializedAsync()
        {
            if (DataService == null)
                return;
            await UpdateValues(DataService.CacheSensorsLiveHome.Value);
            TestStoC = DataService.TestStoC.Subscribe(x => NewEvent(x));
            NotificationUpdate = DataService.CacheSensorsLiveHome.Subscribe(async x => await UpdateValues(x));
            await base.OnInitializedAsync();
        }

        private void NewEvent(int val)
        {
            testValue = val;
            InvokeAsync(() => StateHasChanged());
        }

        private async Task UpdateValues(Data.Models.ValueShown[] newValues)
        {
            ValueShowns = newValues;
            await InvokeAsync(StateHasChanged);
            //if (DataService == null)
            //    return;
            //ValueShowns = await DataService.GetSensorsLive("Home");
        }

        private void SendEvent()
        {
            DataService?.TestCtoS.OnNext(0);
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }


        public void Dispose()
        {
            TestStoC?.Dispose();
            NotificationUpdate?.Dispose();
            GC.SuppressFinalize(this);
        }



    }
}
