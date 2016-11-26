using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Qxr.AutoMapper;
using Qxr.Dependency;
using Qxr.Domain;
using Qxr.Tests.Repositories;
using Qxr.EntityFramework.DbContextStorage;
using Qxr.EntityFramework.Infrastructures;
using Qxr.Reflection;
using Qxr.Logging;
using Qxr.Tests.Repositories.Repositories;

namespace Qxr.Ioc.UnityInject
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    internal class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> unityContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return unityContainer.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IDbContextStorage, HttpDbContextStorage>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof (IDbContextProvider<>), typeof (SimpleDbContextProvider<>), new TransientLifetimeManager());
            container.RegisterType<ITypeFinder, TypeFinder>(new ContainerControlledLifetimeManager());

            container.RegisterType<IUnitOfWork, QxrTestUnitOfWork>();

            //var repoAssemblies = AllClasses.FromAssemblies(Assembly.Load("Qxr.Tests.Repositories")).Where(t => typeof(IDependency).IsAssignableFrom(t));
            //var serviceAssemblies = AllClasses.FromAssemblies(Assembly.Load("Qxr.Services")).Where(t => typeof(IDependency).IsAssignableFrom(t));
            var repoAssemblies = AllClasses.FromAssemblies(Assembly.Load("Qxr.Tests.Repositories")).Where(t => t.Name.EndsWith("Repository"));
            var serviceAssemblies = AllClasses.FromAssemblies(Assembly.Load("Qxr.Services")).Where(t => t.Name.EndsWith("Service"));
            var allAssemblies = repoAssemblies.Concat(serviceAssemblies).ToList();
            container.RegisterType<ILogger, Log4NetAdapter>(new ContainerControlledLifetimeManager());

            var interceptionInjectionMembers = new InjectionMember[]
            {
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<AuditLoggingBehavior>()
            };
            container.RegisterTypes(allAssemblies,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.Transient, t => interceptionInjectionMembers);

            container.RegisterType(typeof(IReadOnlyRepository<>), typeof(QxrTestRepositoryBase<>), interceptionInjectionMembers);
            container.RegisterType(typeof(IRepository<>), typeof(QxrTestRepositoryBase<>), interceptionInjectionMembers);

            //var loggerTypes = allAssemblies.Where(t => t.GetProperties().Any(c => c.PropertyType == typeof(ILogger)));
            //container.RegisterTypes(loggerTypes,
            //    WithMappings.FromMatchingInterface,
            //    WithName.Default,
            //    WithLifetime.Transient, t => new InjectionMember[]
            //    {
            //        new InjectionProperty("Logger")
            //    });

            container.RegisterType<IIocResolver, IocResolver>(new InjectionConstructor(container));
            DbContextProviderFactory.Initialize(container.Resolve<IIocResolver>());
            LoggingFactory.Initialize(container.Resolve<ILogger>());
            new AutoMapperBootstrap(container.Resolve<ITypeFinder>()).CreateMappings();

        }
    }
}
