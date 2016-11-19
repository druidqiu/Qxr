using System;
using System.Linq;
using System.Linq.Expressions;

namespace Qxr.Models.Domain
{
    public interface IReadOnlyRepository<T> where T : IAggregateRoot
    {
        T GetById(object id);
        T Get(Expression<Func<T, bool>> filterExpression);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> filterExpression);
        IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths);
    }
}
