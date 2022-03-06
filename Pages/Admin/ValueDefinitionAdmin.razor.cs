using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using GrinHome.Data;
using Microsoft.EntityFrameworkCore;
using GrinHome.Pages.Components;

namespace GrinHome.Pages.Admin
{
    public partial class ValueDefinitionAdmin : GrinTableEditor<ValueDefinition>
    {
        IEnumerable<Sensor> sensors = Enumerable.Empty<Sensor>();
        IEnumerable<DataType> dataTypes = Enumerable.Empty<DataType>();

        protected override void LoadData()
        {
            items = db.ValueDefinitions.Include(x => x.Sensor).Include(x => x.DataType);
            sensors = db.Sensors.ToList();
            dataTypes = db.DataTypes.ToList();
        }






    }
}
