using Qxr.EntityFramework.DbContextStorage;
using System.Data.Entity;

namespace Qxr.EntityFramework.Infrastructures
{
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext,new()
    {
        private static IDbContextStorage _dbContextStorage;
        private const string StorageKey = "dbContextStorage";

        public SimpleDbContextProvider(IDbContextStorage dbContextStorage)
        {
            _dbContextStorage = dbContextStorage;
        }

        public TDbContext DbContext
        {
            get
            {
                TDbContext dataContext = _dbContextStorage.Retrieve<TDbContext>(StorageKey);
                if (dataContext == null)
                {
                    dataContext = new TDbContext();
                    _dbContextStorage.Store(StorageKey, dataContext);
                }

                return dataContext;
            }
        }

        public void ResetDbContext()
        {
            var dataContext = new TDbContext();
            _dbContextStorage.Store(StorageKey, dataContext);
        }
    }
}
