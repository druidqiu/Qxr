using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using Qxr.Models.Entities;
using Qxr.Repositories.Infrastructures;

namespace Qxr.Repositories
{
    [DbConfigurationType(typeof(EF6CodeConfig))]
    public class QxrDataContext : DbContext
    {
        public QxrDataContext()
            : base("QxrConnectionString")
        {
            //Configuration.LazyLoadingEnabled = false;
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #region Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion Entities

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
        }
    }
}
