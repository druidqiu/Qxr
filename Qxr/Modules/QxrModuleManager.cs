using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Qxr.Logging;
using Qxr.Dependency;

namespace Qxr.Modules
{
    public class QxrModuleManager : IQxrModuleManager
    {
        public ILogger Logger { get; set; }

        private readonly QxrModuleCollection _modules;
        private readonly IIocManager _iocManager;
        private readonly IModuleFinder _moduleFinder;

        public QxrModuleManager(IIocManager iocManager, IModuleFinder moduleFinder)
        {
            _modules = new QxrModuleCollection();
            _iocManager = iocManager;
            _moduleFinder = moduleFinder;
            Logger = NullLogger.Instance;
        }

        public virtual void InitializeModules()
        {
            LoadAll();

            var sortedModules = _modules.GetSortedModuleListByDependency();

            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
        }

        private void LoadAll()
        {
            Logger.Debug("Loading Qxr modules...");

            var moduleTypes = AddMissingDependedModules(_moduleFinder.FindAll());
            Logger.Debug("Found " + moduleTypes.Count + " Qxr modules in total.");

            //Register to IOC container.
            foreach (var moduleType in moduleTypes)
            {
                if (!QxrModule.IsQxrModule(moduleType))
                {
                    throw new QxrException("This type is not an Qxr module: " + moduleType.AssemblyQualifiedName);
                }

                if (!_iocManager.IsRegistered(moduleType))
                {
                    _iocManager.Register(moduleType);
                }
            }

            //Add to module collection
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = (QxrModule) _iocManager.Resolve(moduleType);

                moduleObject.IocManager = _iocManager;

                _modules.Add(new QxrModuleInfo(moduleObject));

                Logger.Debug("Loaded module: " + moduleType.AssemblyQualifiedName);
            }

            EnsureKernelModuleToBeFirst();

            SetDependencies();

            Logger.Debug(_modules.Count + " modules loaded.");
        }

        private void EnsureKernelModuleToBeFirst()
        {
            var kernelModuleIndex = _modules.FindIndex(m => m.Type == typeof(QxrKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = _modules[kernelModuleIndex];
                _modules.RemoveAt(kernelModuleIndex);
                _modules.Insert(0, kernelModule);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //Set dependencies according to assembly dependency
                foreach (var referencedAssemblyName in moduleInfo.Assembly.GetReferencedAssemblies())
                {
                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    var dependedModuleList = _modules.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedModuleList.Count > 0)
                    {
                        moduleInfo.Dependencies.AddRange(dependedModuleList);
                    }
                }

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in QxrModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new QxrException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        private static ICollection<Type> AddMissingDependedModules(ICollection<Type> allModules)
        {
            var initialModules = allModules.ToList();
            foreach (var module in initialModules)
            {
                FillDependedModules(module, allModules);
            }

            return allModules;
        }

        private static void FillDependedModules(Type module, ICollection<Type> allModules)
        {
            foreach (var dependedModule in QxrModule.FindDependedModuleTypes(module))
            {
                if (!allModules.Contains(dependedModule))
                {
                    allModules.Add(dependedModule);
                    FillDependedModules(dependedModule, allModules);
                }
            }
        }         
    }
}
