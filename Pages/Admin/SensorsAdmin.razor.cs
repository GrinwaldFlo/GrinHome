using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using GrinHome.Data;
using Microsoft.EntityFrameworkCore;
using GrinHome.Pages.Components;

namespace GrinHome.Pages.Admin
{
    public partial class SensorsAdmin : GrinTableEditor<Sensor>
    {
        readonly IEnumerable<CommType> comeTypes = Enum.GetValues(typeof(CommType)).Cast<CommType>();

        //IEnumerable<Customer> customers;
        //IEnumerable<Employee> employees;

 
        protected override void LoadData()
        {
            items = db.Sensors;
        }

       

    }
}
