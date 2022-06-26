using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using GrinHome.Data;
using Microsoft.EntityFrameworkCore;
using GrinHome.Pages.Components;

namespace GrinHome.Pages.Admin
{
    public partial class CountersAdmin : GrinTableEditor<CounterItem>
    {
 
        protected override void LoadData()
        {
            items = db.CounterItems;
        }

       

    }
}
