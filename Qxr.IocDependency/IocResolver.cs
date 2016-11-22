using Qxr.Dependency;
using System.Web.Mvc;

namespace Qxr.IocDependency
{
    public class IocResolver : IIocResolver
    {
        public T Resolve<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }


        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public object Resolve(System.Type type, object argumentsAsAnonymousType)
        {
            throw new System.NotImplementedException();
        }
    }
}
