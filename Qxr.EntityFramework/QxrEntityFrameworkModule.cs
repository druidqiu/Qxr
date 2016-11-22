using Qxr.Logging;
using Qxr.Modules;
using Qxr.Reflection;
using Qxr.EntityFramework.DbContextStorage;
using Qxr.EntityFramework.Infrastructures;
using Qxr.EntityFramework.Repositories;
using Qxr.Extensions;

namespace Qxr.EntityFramework
{
    [DependsOn(typeof(QxrKernelModule))]
    public class QxrEntityFrameworkModule : QxrModule
    {
        private ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        public QxrEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PreInitialize()
        {
            //TODO:
            //IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());
        }

        public override void Initialize()
        {
            //IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
            IocManager.Register<IDbContextStorage, HttpDbContextStorage>(Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register(typeof (IDbContextProvider<>), typeof (SimpleDbContextProvider<>), Dependency.DependencyLifeStyle.Transient);

            RegisterGenericRepositories();
        }

        private void RegisterGenericRepositories()
        {
            var dbContextTypes = _typeFinder.Find(type => type.IsPublic && !type.IsAbstract && type.IsClass && typeof (QxrDbContext).IsAssignableFrom(type));
            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from QxrDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
        }
    }
}
