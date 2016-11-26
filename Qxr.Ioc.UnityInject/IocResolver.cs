using Microsoft.Practices.Unity;
using Qxr.Dependency;

namespace Qxr.Ioc.UnityInject
{
    public class IocResolver : IIocResolver
    {
        private readonly IUnityContainer _unityContainer;
        public IocResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
