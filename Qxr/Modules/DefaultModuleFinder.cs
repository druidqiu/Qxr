using System;
using System.Collections.Generic;
using System.Linq;
using Qxr.Reflection;

namespace Qxr.Modules
{
    internal class DefaultModuleFinder : IModuleFinder
    {
        private readonly ITypeFinder _typeFinder;

        public DefaultModuleFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(QxrModule.IsQxrModule).ToList();
        }
    }
}
