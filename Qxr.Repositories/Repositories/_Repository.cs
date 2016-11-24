using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Qxr.Domain;
using EntityFramework.Extensions;
using Qxr.Repositories.Infrastructures;

namespace Qxr.Repositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot, new()
    {
        private readonly QxrDataContext _dbContext;
        private readonly IDbSet<T> _dbSet;

        protected Repository()
        {
            _dbContext = DataContextFactory.GetDataContext();
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                _dbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void BulkUpdateByExpression(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            _dbSet.Where(filterExpression).Update(updateExpression);
        }

        public void BulkUpdate(IQueryable<T> query, Expression<Func<T, T>> updateExpression)
        {
            query.Update(updateExpression);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void BulkDelete(IQueryable<T> query)
        {
            query.Delete();
        }

        public void BulkDeleteByExpression(Expression<Func<T, bool>> filterExpression)
        {
            _dbSet.Where(filterExpression).Delete();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filterExpression)
        {
            return _dbSet.AsNoTracking().Where(filterExpression).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> filterExpression)
        {
            return _dbSet.AsNoTracking().Where(filterExpression);
        }

        public IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths)
        {
            var query = GetAll();
            return paths.Aggregate(query, (current, path) => current.Include(path));
        }
    }
}
