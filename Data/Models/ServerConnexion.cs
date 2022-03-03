using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ServerConnexion
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }


        [StringLength(50)]
        public string User { get; set; }
         
        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string URL { get; set; }

        public int Port { get; set; }

        [StringLength(200)]
        public string Topic { get; set; }

        public CommType ServerType { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}