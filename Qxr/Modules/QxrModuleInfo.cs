using System;
using System.Collections.Generic;
using System.Reflection;

namespace Qxr.Modules
{
    internal class QxrModuleInfo
    {
        public Assembly Assembly { get; private set; }
        public Type Type { get; private set; }
        public QxrModule Instance { get; private set; }
        public List<QxrModuleInfo> Dependencies { get; private set; }

        public QxrModuleInfo(QxrModule instance)
        {
            Dependencies = new List<QxrModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
