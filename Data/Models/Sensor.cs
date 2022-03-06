using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class Sensor
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string DeviceName { get; set; } = null!;

        public CommType CommType { get; set; }

        public virtual IList<ValueDefinition> ValueDefinitions { get; set; } = new List<ValueDefinition>();

        public override string ToString()
        {
            return $"{ID} - {Name}/{DeviceName} ({CommType})";
        }
    }
}