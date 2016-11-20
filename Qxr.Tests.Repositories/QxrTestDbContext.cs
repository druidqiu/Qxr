using Qxr.EntityFramework;
using Qxr.Models.Entities;
using System.Data.Entity;

namespace Qxr.Tests.Repositories
{
    public class QxrTestDbContext : QxrDbContext
    {
        public QxrTestDbContext()
            : base("QxrConnectionString")
        {}

        public override void Initializer()
        {
            Database.SetInitializer<QxrTestDbContext>(null);
            //Database.SetInitializer(new QxrTestDbInitializer());
        }

        #region Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion Entities
    }
}
