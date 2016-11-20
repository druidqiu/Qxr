using Qxr.Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Qxr.EntityFramework.UnitOfWork
{
    public class EfUnitOfWork<TDbContext> : UnitOfWorkBase, IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        public TDbContext Context { get { return _dbContextProvider.DbContext; } }
        private DbContextTransaction _transaction;

        public EfUnitOfWork(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _transaction = Context.Database.CurrentTransaction ?? Context.Database.BeginTransaction();
        }

        public override bool Commit()
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
                return false;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                return false;
            }
            finally
            {
                _dbContextProvider.ResetDbContext();
            }
        }
    }
}
