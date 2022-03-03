using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class Sensor
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceName { get; set; }

        public CommType CommType { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public virtual IList<ValueDefinition> ValueDefinitions { get; set; } = new List<ValueDefinition>();
    }
}