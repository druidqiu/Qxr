using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using Qxr.EntityFramework.Infrastructures;
using Qxr.Logging;

namespace Qxr.EntityFramework
{
    [DbConfigurationType(typeof(Configure))]
    public abstract class QxrDbContext : DbContext
    {
        public ILogger Logger { get; set; }

        public QxrDbContext()
        {
            Logger = NullLogger.Instance;
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Initializer();
        }

        public QxrDbContext(string connectionString)
            : base(connectionString)
        {
            //TODO: DRY
            Logger = NullLogger.Instance;
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Initializer();
        }

        public virtual void Initializer()
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
