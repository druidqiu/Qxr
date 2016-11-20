using Qxr.Dependency;
using System.Data.Entity;

namespace Qxr.EntityFramework.Infrastructures
{
    public static class DbContextProviderFactory
    {
        private static IIocResolver _iocResolver;

        public static void Initialize(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public static IDbContextProvider<TDbContext> GetDbContextProvider<TDbContext>()
            where TDbContext : DbContext
        {
            return _iocResolver.Resolve<IDbContextProvider<TDbContext>>();
        }
    }
}
