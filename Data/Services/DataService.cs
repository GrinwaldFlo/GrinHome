using GrinHome.Data.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Subjects;

namespace GrinHome.Data.Services
{
    public class DataService
    {
        public Subject<int> TestCtoS { get; } = new();
        public Subject<int> TestStoC { get; } = new();

        public Subject<CommType> NotificationUpdate { get; } = new();

        public BehaviorSubject<ValueShown[]> CacheSensorsLiveHome = new(Array.Empty<ValueShown>());

        private IServiceProvider ServiceProvider { get; }

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

        public async Task<DataGraph[]> GetTrends(string page, TimeSpan timeSpan)
        {
            var db = Db;
            var result = new List<DataGraph>();
            if (db == null)
                return result.ToArray();

            var valuesShown = await db.ValuesShown.Where(x => x.Page == page)
                .Include(x => x.ValueDefinition).ThenInclude(x => x.DataType)
                .OrderBy(x => x.Order)
                .AsSplitQuery().AsNoTracking().ToArrayAsync();

            foreach (var item in valuesShown)
            {
                DateTime from = DateTime.Now - timeSpan;
                DateTime oldFrom = from.AddYears(-1);
                DateTime oldTo = oldFrom + timeSpan;

                var values = await db.SensorValues.Where(x => x.Date > from && x.ValueDefinitionID == item.ValueDefinition.ID).OrderBy(x => x.Date).ToArrayAsync();
                var oldValues = await db.SensorValues.Where(x => x.Date > oldFrom && x.Date < oldTo && x.ValueDefinitionID == item.ValueDefinition.ID)
                    .OrderBy(x => x.Date).ToListAsync();

                oldValues.ForEach(x => x.Date = x.Date.AddYears(1));

                result.Add(new DataGraph()
                {
                    ValueDefinition = item.ValueDefinition,
                    Data = values,
                    DataOld = oldValues
                });
            }
            return result.ToArray();
        }

        public DataService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<SensorValue[]> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new SensorValue
            {
                Date = startDate.AddDays(index),
                Value = Random.Shared.Next(-20, 55),
            }).ToArray());
        }

        public async Task<ValueShown[]> GetSensorsLive(string page)
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();
            var db = Db;
            if (db == null)
                return Array.Empty<ValueShown>();

            var result = await db.ValuesShown.Where(x => x.Page == page)
                .Include(x => x.ValueDefinition).ThenInclude(y => y.SensorValues.OrderByDescending(d => d.Date).Take(1))
                .Include(x => x.ValueDefinition).ThenInclude(x => x.DataType)
                .OrderBy(x => x.Order)
                .AsSplitQuery().AsNoTracking().ToArrayAsync();

            //   stopwatch.Stop();
            // Log.Debug($"GetSensorsLive: {stopwatch.ElapsedMilliseconds} ms.");
            return result;
        }
    }
}