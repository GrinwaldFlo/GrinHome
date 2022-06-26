using GrinHome.Data.Models;

namespace GrinHome.Data
{
    public class DataItem
    {
        public DateTime Date { get; set; }
        public float Value { get; set; }
    }

    public class DataGraph
    {
        public IList<SensorValue> Data { get; set; } = new List<SensorValue>();
        public IList<SensorValue> DataOld { get; set; } = new List<SensorValue>();

        public ValueDefinition? ValueDefinition { get; set; }


    }
}
