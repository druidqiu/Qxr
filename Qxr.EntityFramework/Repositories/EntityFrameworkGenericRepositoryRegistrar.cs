using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qxr.Dependency;
using Qxr.Reflection.Extentsions;
using Qxr.EntityFramework.Extensions;

namespace Qxr.EntityFramework.Repositories
{
    internal static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>();
            if (autoRepositoryAttr == null)
            {
                autoRepositoryAttr = AutoRepositoryTypesAttribute.Default;
            }

            foreach (var entityType in dbContextType.GetEntityTypes())
            {
                var genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityType);
                if (!iocManager.IsRegistered(genericRepositoryType))
                {
                    var implType = autoRepositoryAttr.RepositoryImplementation.GetGenericArguments().Length == 1
                        ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityType)
                        : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(dbContextType, entityType);

                    iocManager.Register(
                        genericRepositoryType,
                        implType,
                        DependencyLifeStyle.Transient
                        );
                }
            }
        }
    }
}
