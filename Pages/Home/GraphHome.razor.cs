using GrinHome.Data;
using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;

namespace GrinHome.Pages.Home
{
    public partial class GraphHome : ComponentBase
    {

        [Parameter] public string? DataPage { get; set; }
        [Inject] DataService? DataService { get; set; }

        private DataGraph[] dataGraphs = Array.Empty<DataGraph>();

        private string[]? DeviceList = Array.Empty<string>();
        private int TimeSpanHours = 24;


        protected override async Task OnParametersSetAsync()
        {
            if (DataService == null)
                return;

            DeviceList = DataService.Db?.Sensors.Select(x => x.Name).ToArray();

            if (DeviceList == null || !DeviceList.Any())
                return;

            if (string.IsNullOrEmpty(DataPage))
                DataPage = DeviceList.First();

            await UpdateTrend();

            await base.OnParametersSetAsync();
        }

        private async void SetTimeSpanHours(int hours)
        {
            TimeSpanHours = hours;
            await UpdateTrend();
        }

        private async void SetPage(string page)
        {
            DataPage = page;
            await UpdateTrend();
        }

        private async Task UpdateTrend()
        {
            if (DataService == null || string.IsNullOrEmpty(DataPage))
                return;
            dataGraphs = await DataService.GetTrends(DataPage, TimeSpan.FromHours(TimeSpanHours));
            StateHasChanged();
        }
    }
}
