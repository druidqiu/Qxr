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
    }
}
