using Serilog;

namespace GrinHome.Data.Models
{
    public class SensorValue
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }

        public ushort ValueDefinitionID { get; set; }

        public virtual ValueDefinition ValueDefinition { get; set; } = null!;


        public override string ToString()
        {
            return $"{ValueDefinition?.Name} {Date} - {Value}";
        }
    }
}