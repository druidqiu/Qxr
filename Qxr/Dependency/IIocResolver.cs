using System;

namespace Qxr.Dependency
{
    public interface IIocResolver
    {
        T Resolve<T>();
        T Resolve<T>(object argumentsAsAnonymousType);
        object Resolve(Type type);
        object Resolve(Type type, object argumentsAsAnonymousType);
    }
}
