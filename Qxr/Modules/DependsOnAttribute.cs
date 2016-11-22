using System;

namespace Qxr.Modules
{
    public class DependsOnAttribute : Attribute
    {
        public Type[] DependedModuleTypes { get; private set; }

        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
