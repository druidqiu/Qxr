using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Qxr.AutoMapper;
using Qxr.Reflection;
using Qxr.Models.Domain;
using Qxr.Repositories.Infrastructures;

namespace Qxr.IocDependency
{
    public static class IocManager
    {
        public static void RegisterDependency(Assembly mvcWebAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mvcWebAssembly);
            var container = SetupResolveRules(builder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static T GetInstance<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        private static IContainer SetupResolveRules(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IReadOnlyRepository<>)).InstancePerDependency();
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.Load("Qxr.Repositories")).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Qxr.Services")).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<HttpStorage>().As<IDataStorage>().SingleInstance();
            builder.RegisterType<TypeFinder>().As<ITypeFinder>().SingleInstance();

            var container = builder.Build();

            DataContextFactory.Initialize(container.Resolve<IDataStorage>());
            new AutoMapperBootstrap(container.Resolve<ITypeFinder>()).CreateMappings();

            return container;
        }
    }
}
