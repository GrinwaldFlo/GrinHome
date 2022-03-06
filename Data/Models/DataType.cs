using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class DataType
    {
        public ushort ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(5)]
        public string Unit { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string JsonPath { get; set; } = null!;
    }
}