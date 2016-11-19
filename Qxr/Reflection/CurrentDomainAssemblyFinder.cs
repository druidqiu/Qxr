using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qxr.Reflection
{
    public class CurrentDomainAssemblyFinder : IAssemblyFinder
    {
        public static CurrentDomainAssemblyFinder Instance { get { return SingletonInstance; } }
        private static readonly CurrentDomainAssemblyFinder SingletonInstance = new CurrentDomainAssemblyFinder();

        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
