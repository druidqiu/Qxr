using System.Collections.Generic;
using System.Linq;
using Qxr.Extensions;

namespace Qxr.Modules
{
    internal class QxrModuleCollection : List<QxrModuleInfo>
    {
        public TModule GetModule<TModule>() where TModule : QxrModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof (TModule));
            if (module == null)
            {
                throw new QxrException("Can not find module for " + typeof (TModule).FullName);
            }

            return (TModule) module.Instance;
        }

        public List<QxrModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }

        private static void EnsureKernelModuleToBeFirst(List<QxrModuleInfo> sortedModules)
        {
            var kernelModuleIndex = sortedModules.FindIndex(m => m.Type == typeof(QxrKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = sortedModules[kernelModuleIndex];
                sortedModules.RemoveAt(kernelModuleIndex);
                sortedModules.Insert(0, kernelModule);
            }
        }
    }
}
