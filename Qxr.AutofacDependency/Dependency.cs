using Qxr.AutoMapper;
using Qxr.Dependency;
using Qxr.EntityFramework.Infrastructures;
using Qxr.MvcAssist;
using Qxr.Reflection;
using System.Reflection;
using Autofac;
using Qxr.Domain;
using Qxr.EntityFramework.UnitOfWork;
using Qxr.Tests.Repositories;

namespace Qxr.AutofacDependency
{
    public class Dependency
    {
        private QxrBootstrapper QxrBootstrapper { get; set; }
        private IocManager IocManager { get; set; }

        public Dependency()
        {
            IocManager = IocManager.Instance;
        }

        public void Register(Assembly mvcWebAssembly)
        {
            IocManager.RegisterControllers(mvcWebAssembly);
            IocManager.Instance.SetMvcResolver();
            QxrBootstrapper = new QxrBootstrapper(IocManager);
            QxrBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
            QxrBootstrapper.Initialize();

            IocManager.IocContainerBuilder.RegisterType<EfUnitOfWork<QxrTestDbContext>>().As<IUnitOfWork>().InstancePerDependency();
            IocManager.IocContainerBuilder.RegisterAssemblyTypes(Assembly.Load("Qxr.Services")).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            

            new AutoMapperBootstrap(IocManager.Resolve<ITypeFinder>()).CreateMappings();
        }

        public void Shutdown()
        {
            QxrBootstrapper.Dispose();
        }
    }
}
