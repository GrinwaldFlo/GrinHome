using GrinHome.Data;
using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;

namespace GrinHome.Pages.Components
{
    public abstract class GrinTableEditor<T> : ComponentBase where T : class, new()
    {
        protected ApplicationDbContext db = null!;
        protected IEnumerable<T> items = Enumerable.Empty<T>();
        protected RadzenDataGrid<T> itemsGrid = null!;
        protected T? itemToInsert;
        [Inject] protected DataService? DS { get; set; }
        [Inject] protected NavigationManager Nav { get; set; } = null!;

        protected void CancelEdit(T item)
        {
            if (item == itemToInsert)
            {
                itemToInsert = null;
            }

            itemsGrid.CancelEditRow(item);

            var orderEntry = db.Entry(item);
            if (orderEntry.State == EntityState.Modified)
            {
                orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
                orderEntry.State = EntityState.Unchanged;
            }
        }

        protected async Task DeleteRow(T item)
        {
            if (item == itemToInsert)
            {
                itemToInsert = null;
            }

            if (items.Contains(item))
            {
                db.Remove<T>(item);
                db.SaveChanges();

                await itemsGrid.Reload();
            }
            else
            {
                itemsGrid.CancelEditRow(item);
            }
        }

        protected async Task EditRow(T item)
        {
            await itemsGrid.EditRow(item);
        }

        protected async Task InsertRow()
        {
            itemToInsert = new T();
            await itemsGrid.InsertRow(itemToInsert);
        }

        protected abstract void LoadData();

        protected void OnCreateRow(T item)
        {
            db.Add(item);
            db.SaveChanges();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ApplicationDbContext? newDb = DS?.Db;

            if (newDb == null)
            {
                Nav.NavigateTo("/");
                return;
            }
            db = newDb;
            LoadData();
        }
        protected void OnUpdateRow(T item)
        {
            if (item == itemToInsert)
            {
                itemToInsert = null;
            }

            db.Update(item);

            db.SaveChanges();
        }

        protected async Task SaveRow(T item)
        {
            if (item == itemToInsert)
            {
                itemToInsert = null;
            }

            await itemsGrid.UpdateRow(item);
        }
    }
}
