using Qxr.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace Qxr.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity> : QxrRepositoryBase<TEntity>
        where TEntity : class, IAggregateRoot, new()
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override void Add(TEntity entity)
        {
            Table.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                Table.Add(entity);
            }
        }

        public override void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public override void BulkUpdate(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            Table.Where(filterExpression).Update(updateExpression);
        }

        public override void BulkUpdate(IQueryable<TEntity> query, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            query.Update(updateExpression);
        }

        public override void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public override void BulkDelete(IQueryable<TEntity> query)
        {
            query.Delete();
        }

        public override void BulkDelete(Expression<Func<TEntity, bool>> filterExpression)
        {
            Table.Where(filterExpression).Delete();
        }

        public override TEntity GetById(object id)
        {
            return Table.Find(id);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsNoTracking();
        }

        public override IQueryable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] paths)
        {
            var query = GetAll();
            return paths.Aggregate(query, (current, path) => current.Include(path));
        }
    }
}
