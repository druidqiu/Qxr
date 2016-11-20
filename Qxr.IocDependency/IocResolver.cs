using Qxr.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
