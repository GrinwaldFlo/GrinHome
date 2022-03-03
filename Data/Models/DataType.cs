using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class DataType
    {
        public ushort ID { get; set; }

        [Required]
        [StringLength(20)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }

        [Required]
        [StringLength(5)]
        public string Unit { get; set; }

        [Required]
        [StringLength(100)]
        public string JsonPath { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    }
}