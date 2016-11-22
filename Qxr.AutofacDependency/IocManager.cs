using System;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Qxr.Dependency;

namespace Qxr.AutofacDependency
{
    /// <summary>
    /// This class is used to directly perform dependency injection tasks.
    /// </summary>
    internal class IocManager : IIocManager
    {
        public static IocManager Instance { get; private set; }
        public ContainerBuilder IocContainerBuilder { get; set; }

        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainerBuilder = new ContainerBuilder();
            IocContainerBuilder.RegisterType<IocManager>().As<IIocManager>();
        }

        public void RegisterAssembly(Assembly assembly)
        {
            IocContainerBuilder.RegisterAssemblyTypes(assembly);
        }

        public void RegisterMvcAssembly(Assembly assembly)
        {
            IocContainerBuilder.RegisterControllers(assembly);
        }

        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default) where T : class
        {
            if (lifeStyle == DependencyLifeStyle.Default)
                IocContainerBuilder.RegisterType<T>().InstancePerDependency();
            if (lifeStyle == DependencyLifeStyle.Singleton)
                IocContainerBuilder.RegisterType<T>().SingleInstance();
            if (lifeStyle == DependencyLifeStyle.Transient)
                IocContainerBuilder.RegisterType<T>().InstancePerLifetimeScope();
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default)
        {
            if (lifeStyle == DependencyLifeStyle.Default)
                IocContainerBuilder.RegisterType(type).InstancePerDependency();
            if (lifeStyle == DependencyLifeStyle.Singleton)
                IocContainerBuilder.RegisterType(type).SingleInstance();
            if (lifeStyle == DependencyLifeStyle.Transient)
                IocContainerBuilder.RegisterType(type).InstancePerLifetimeScope();
        }

        public void Register<T, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default)
            where T : class
            where TImpl : class, T
        {
            if (lifeStyle == DependencyLifeStyle.Default)
                IocContainerBuilder.RegisterType<TImpl>().As<T>().InstancePerDependency();
            if (lifeStyle == DependencyLifeStyle.Singleton)
                IocContainerBuilder.RegisterType<TImpl>().As<T>().SingleInstance();
            if (lifeStyle == DependencyLifeStyle.Transient)
                IocContainerBuilder.RegisterType<TImpl>().As<T>().InstancePerLifetimeScope();
        }

        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default)
        {
            if (lifeStyle == DependencyLifeStyle.Default)
            IocContainerBuilder.RegisterType(impl).As(type).InstancePerDependency();
            if (lifeStyle == DependencyLifeStyle.Singleton)
                IocContainerBuilder.RegisterType(impl).As(type).SingleInstance();
            if(lifeStyle == DependencyLifeStyle.Transient)
                IocContainerBuilder.RegisterType(impl).As(type).InstancePerLifetimeScope();
        }

        public bool IsRegistered(Type type)
        {
            return false;
        }

        public bool IsRegistered<T>()
        {
            return false;
        }

        public T Resolve<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return DependencyResolver.Current.GetService<T>();
        }

        public object Resolve(Type type)
        {
            return DependencyResolver.Current.GetService(type);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return DependencyResolver.Current.GetService(type);
        }

        public void RegisterControllers(Assembly assembly)
        {
            IocContainerBuilder.RegisterControllers(assembly);
        }

        public void SetMvcResolver()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocContainerBuilder.Build()));
        }
    }
}
