using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ServerConnexion
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;


        [StringLength(50)]
        public string User { get; set; } = null!;

        [StringLength(200)]
        public string Password { get; set; } = null!;

        [StringLength(200)]
        public string URL { get; set; } = null!;

        public int Port { get; set; }

        [StringLength(200)]
        public string Topic { get; set; } = null!;

        public CommType ServerType { get; set; }

    }
}