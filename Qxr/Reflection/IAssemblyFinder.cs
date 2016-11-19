using System.Collections.Generic;
using System.Reflection;

namespace Qxr.Reflection
{
    public interface IAssemblyFinder
    {
        List<Assembly> GetAllAssemblies();
    }
}
