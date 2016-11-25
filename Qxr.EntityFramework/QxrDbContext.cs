using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using Qxr.EntityFramework.Infrastructures;

namespace Qxr.EntityFramework
{
    [DbConfigurationType(typeof(Configure))]
    public abstract class QxrDbContext : DbContext
    {
        protected QxrDbContext(string connectionString)
            : base(connectionString)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;//load sql.dll
            Initializer();
        }

        protected virtual void Initializer()
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
        }
    }
}
