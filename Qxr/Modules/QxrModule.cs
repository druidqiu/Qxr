using System;
using System.Collections.Generic;
using System.Linq;
using Qxr.Dependency;

namespace Qxr.Modules
{
    public abstract class QxrModule
    {
        public IIocManager IocManager { get; internal set; }

        public virtual void PreInitialize()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void PostInitialize()
        {

        }

        public virtual void Shutdown()
        {

        }

        public static bool IsQxrModule(Type type)
        {
            return type.IsClass && !type.IsAbstract && typeof (QxrModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds depended modules of a module.
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsQxrModule(moduleType))
            {
                throw new QxrException("This type is not an Qxr module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}
