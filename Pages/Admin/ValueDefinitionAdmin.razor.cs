using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using GrinHome.Data;
using Microsoft.EntityFrameworkCore;

namespace GrinHome.Pages.Admin
{
    public partial class ValueDefinitionAdmin : ComponentBase
    {
        [Inject] DataService? DataService { get; set; }
        [Inject] protected NavigationManager Nav { get; set; } = null!;

        RadzenDataGrid<ValueDefinition> sensorsGrid = null!;
        IEnumerable<ValueDefinition> sensors = Enumerable.Empty<ValueDefinition>();
        ValueDefinition? sensorToInsert;
        private ApplicationDbContext db = null!;

        readonly IEnumerable<CommType> comeTypes = Enum.GetValues(typeof(CommType)).Cast<CommType>();

        //IEnumerable<Customer> customers;
        //IEnumerable<Employee> employees;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ApplicationDbContext? newDb = DataService?.Db;

            if (newDb == null)
            {
                Nav.NavigateTo("/");
                return;
            }
            db = newDb;
            sensors = db.ValueDefinitions.Include(x => x.Sensor);
        }

        async Task EditRow(ValueDefinition order)
        {
            await sensorsGrid.EditRow(order);
        }

        void OnUpdateRow(ValueDefinition order)
        {
            if (order == sensorToInsert)
            {
                sensorToInsert = null;
            }

            db.Update(order);

            db.SaveChanges();
        }

        async Task SaveRow(ValueDefinition order)
        {
            if (order == sensorToInsert)
            {
                sensorToInsert = null;
            }

            await sensorsGrid.UpdateRow(order);
        }

        void CancelEdit(ValueDefinition order)
        {
            if (order == sensorToInsert)
            {
                sensorToInsert = null;
            }

            sensorsGrid.CancelEditRow(order);

            var orderEntry = db.Entry(order);
            if (orderEntry.State == EntityState.Modified)
            {
                orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
                orderEntry.State = EntityState.Unchanged;
            }
        }

        async Task DeleteRow(ValueDefinition order)
        {
            if (order == sensorToInsert)
            {
                sensorToInsert = null;
            }

            if (sensors.Contains(order))
            {
                db.Remove<ValueDefinition>(order);
                db.SaveChanges();

                await sensorsGrid.Reload();
            }
            else
            {
                sensorsGrid.CancelEditRow(order);
            }
        }



        async Task InsertRow()
        {
            sensorToInsert = new ValueDefinition();
            await sensorsGrid.InsertRow(sensorToInsert);
        }

        void OnCreateRow(ValueDefinition order)
        {
            db.Add(order);
            db.SaveChanges();
        }

    }
}
