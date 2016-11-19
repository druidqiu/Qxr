using System.Data.Entity;
using Qxr.Models.Entities;
using System.Collections.Generic;

namespace Qxr.Repositories
{
    public class QxrDataInitializer : DropCreateDatabaseIfModelChanges<QxrDataContext>
    {
        protected override void Seed(QxrDataContext context)
        {
            var user = new User { Code = "admin", Name = "Administrator"};
            context.Users.Add(user);

            var roles = new List<Role>
            {
                new Role{ RoleName = RoleNames.Admin, Name = "Admin"},
                new Role{ RoleName = RoleNames.BuTester, Name = "BuTester"},
                new Role{ RoleName = RoleNames.Tester, Name = "Tester"},
            };
            context.Roles.AddRange(roles);
            user.Roles.AddRange(roles);

            context.SaveChanges();
        }
    }
}
