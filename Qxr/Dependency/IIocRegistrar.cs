using System;
using System.Reflection;

namespace Qxr.Dependency
{
    public interface IIocRegistrar
    {
        void RegisterAssembly(Assembly assembly);
        void RegisterMvcAssembly(Assembly assembly);
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default) 
            where T : class;
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default);
        void Register<T, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default)
            where T : class
            where TImpl : class, T;
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Default);
        bool IsRegistered(Type type);
        bool IsRegistered<T>();
    }
}
