using System;
using System.Collections.Generic;

namespace Qxr.Modules
{
    public interface IModuleFinder
    {
        ICollection<Type> FindAll();
    }
}
