using Qxr.EntityFramework.UnitOfWork;
using Qxr.EntityFramework.Infrastructures;

namespace Qxr.Tests.Repositories
{
    public class QxrTestUnitOfWork : EfUnitOfWork<QxrTestDbContext>
    {
        public QxrTestUnitOfWork(IDbContextProvider<QxrTestDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
