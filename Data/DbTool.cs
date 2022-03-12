using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GrinHome.Data
{
    public class DbTool
    {
        public const string Admin = "Admin";
        public const string User = "User";


        public static void CheckDb(ApplicationDbContext db, string adminEmail)
        {
            db.Database.Migrate();

            CheckRole(db, User);
            CheckRole(db, Admin);
           
            var adminUser = db.Users.FirstOrDefault(x => x.NormalizedEmail == adminEmail.ToUpper());
            if (adminUser != null)
            {
                var roleAdmin = db.Roles.First(x => x.Name == Admin);

                if (!db.UserRoles.Any(x => x.UserId == adminUser.Id && x.RoleId == roleAdmin.Id))
                {
                    db.UserRoles.Add(new IdentityUserRole<string>() { UserId = adminUser.Id, RoleId = roleAdmin.Id });
                    db.SaveChanges();
                }
            }
        }

        private static void CheckRole(ApplicationDbContext db, string name)
        {
            if (db.Roles.Any(x => x.NormalizedName == name.ToUpper()))
                return;

            db.Roles.Add(
                new IdentityRole() { Name = name, NormalizedName = name.ToUpper(), Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() }
                );
            db.SaveChanges();
        }

        public static List<IdentityUser> GetUsersFromRole(ApplicationDbContext db, string role)
        {
            var roleDb = db.Roles.FirstOrDefault(x => x.NormalizedName == role.ToUpper());
            if (roleDb == null)
                return new List<IdentityUser>();
            var userRoles = db.UserRoles.Where(x => x.RoleId == roleDb.Id).Select(x => x.UserId).ToList();

            return db.Users.Where(x => userRoles.Contains(x.Id)).ToList();
        }
    }
}
