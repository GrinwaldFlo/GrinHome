using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ValueDefinition
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public Sensor Sensor { get; set; }
        public DataType DataType { get; set; }

        public float Min { get; set; }
        public float Max { get; set; }

        public float LimitMin { get; set; }
        public float LimitMax { get; set; }


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual IList<SensorValue> SensorValues { get; set; } = new List<SensorValue>();


    }
}