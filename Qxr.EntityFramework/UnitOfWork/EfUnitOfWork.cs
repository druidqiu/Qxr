using Qxr.Domain;
using Qxr.EntityFramework.Infrastructures;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Qxr.EntityFramework.UnitOfWork
{
    public class EfUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        private TDbContext Context { get { return _dbContextProvider.DbContext; } }
        private readonly DbContextTransaction _transaction;

        public EfUnitOfWork(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _transaction = Context.Database.CurrentTransaction ?? Context.Database.BeginTransaction();
        }

        public bool Commit()
        {
            try
            {
                bool flag = Context.SaveChanges() > 0;
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
                return flag;
            }
            catch (DbEntityValidationException dvex)
            {
                var errors = dvex.EntityValidationErrors.SelectMany(m => m.ValidationErrors).Select(m => m.ErrorMessage);
                //TODO:Log.Error(errors)
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                _dbContextProvider.ResetDbContext();

                return false;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                _dbContextProvider.ResetDbContext();

                return false;
            }
        }
    }
}
