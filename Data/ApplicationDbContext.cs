using GrinHome.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GrinHome.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<SensorValue> SensorValues { get; set; } = null!;
        public DbSet<DataType> DataTypes { get; set; } = null!;
        public DbSet<ServerConnexion> ServerConnexions { get; set; } = null!;
        public DbSet<ValueDefinition> ValueDefinitions { get; set; } = null!;
        public DbSet<ValueShown> ValuesShown { get; set; } = null!;
        public DbSet<Postfix> Postfixes { get; set; } = null!;
        public DbSet<CounterItem> CounterItems { get; set; } = null!;
        public DbSet<CounterValue> CounterValues { get; set; } = null!;
    }
}