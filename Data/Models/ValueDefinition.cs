using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ValueDefinition
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        public Sensor Sensor { get; set; } = null!;
        public DataType DataType { get; set; } = null!;

        public float Min { get; set; }
        public float Max { get; set; }

        public float LimitMin { get; set; }
        public float LimitMax { get; set; }

        public virtual IList<SensorValue> SensorValues { get; set; } = new List<SensorValue>();

        public override string ToString()
        {
            return $"{Name} - {DataType} - {Sensor}";
        }
    }
}