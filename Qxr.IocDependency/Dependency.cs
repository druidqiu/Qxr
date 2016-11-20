using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Qxr.AutoMapper;
using Qxr.Reflection;
using Qxr.Domain;
using Qxr.EntityFramework.DbContextStorage;
using Qxr.EntityFramework.UnitOfWork;
using Qxr.Tests.Repositories;
using Qxr.Dependency;
using Qxr.EntityFramework.Infrastructures;

namespace Qxr.IocDependency
{
    public static class IocManager
    {
        //TODO:将注入分散到每一个程序集中
        public static void RegisterDependency(Assembly mvcWebAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mvcWebAssembly);
            var container = SetupResolveRules(builder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<HttpDbContextStorage>().As<IDbContextStorage>().SingleInstance();
            builder.RegisterGeneric(typeof(SimpleDbContextProvider<>)).As(typeof(IDbContextProvider<>)).InstancePerDependency();
            builder.RegisterType<EfUnitOfWork<QxrTestDbContext>>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<TypeFinder>().As<ITypeFinder>().SingleInstance();
            builder.RegisterType<IocResolver>().As<IIocResolver>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("Qxr.Tests.Repositories")).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Qxr.Services")).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            //builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            
            var container = builder.Build();
            new AutoMapperBootstrap(container.Resolve<ITypeFinder>()).CreateMappings();
            DbContextProviderFactory.Initialize(container.Resolve<IIocResolver>());

            return container;
        }
    }
}
