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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }

        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<SensorValue> SensorValues { get; set; } = null!;
        public DbSet<DataType> DataTypes { get; set; } = null!;
        public DbSet<ServerConnexion> ServerConnexions { get; set; } = null!;
        public DbSet<ValueDefinition> ValueDefinitions { get; set; } = null!;
        public DbSet<ValueShown> ValuesShown { get; set; } = null!;
        public DbSet<Postfix> Postfixes { get; set; } = null!;


    }
}