using Qxr.Modules;
using Qxr.Reflection;

namespace Qxr.Dependency.Installers
{
    public class QxrCoreInstaller
    {
        private readonly IIocManager IocManager;
        public QxrCoreInstaller(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Instal()
        {
            IocManager.Register<ITypeFinder, TypeFinder>(DependencyLifeStyle.Singleton);
            IocManager.Register<IModuleFinder, DefaultModuleFinder>(DependencyLifeStyle.Singleton);
            IocManager.Register<IQxrModuleManager, QxrModuleManager>(DependencyLifeStyle.Singleton);
        }
    }
}
