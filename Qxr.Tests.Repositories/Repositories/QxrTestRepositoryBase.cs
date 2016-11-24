using Qxr.Domain;
using Qxr.EntityFramework.Repositories;

namespace Qxr.Tests.Repositories.Repositories
{
    public abstract class QxrTestRepositoryBase<TEntity> : EfRepositoryBase<QxrTestDbContext, TEntity>
        where TEntity : class, IAggregateRoot
    {
    }
}