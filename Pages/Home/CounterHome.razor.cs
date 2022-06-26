using GrinHome.Data;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace GrinHome.Pages.Home
{
    public partial class CounterHome : ComponentBase
    {

        //[Parameter] public string? DataPage { get; set; }
        [Inject] DataService? DataService { get; set; }

        private DataGraph[] dataGraphs = Array.Empty<DataGraph>();

        private CounterItem[] counters = Array.Empty<CounterItem>();
        private int TimeSpanHours = 24;


        protected override async Task OnParametersSetAsync()
        {
            if (DataService == null)
                return;

            await UpdateTrend();

            await base.OnParametersSetAsync();
        }

        private async void SetTimeSpanHours(int hours)
        {
            TimeSpanHours = hours;
            await UpdateTrend();
        }

        private async Task UpdateTrend()
        {
            var db = DataService?.Db;
            if (db == null)
                return;

            var _counters = await db.CounterItems
                    .AsSplitQuery().AsNoTracking().ToArrayAsync();
            foreach (var counter in _counters)
            {
                counter.Values = await db.CounterValues.Where(x => x.CounterItemID == counter.ID && x.Date > counter.FirstDay).ToListAsync();
            }

            if (_counters == null)
                return;
            counters = _counters;

            StateHasChanged();
        }

        private async Task AddValue(CounterItem counterItem)
        {
            if (counterItem.ValueToAdd == 0)
                return;

            await counterItem.AddValue(DateTime.Now, counterItem.ValueToAdd, DataService?.Db);
            await UpdateTrend();
        }
    }
}
