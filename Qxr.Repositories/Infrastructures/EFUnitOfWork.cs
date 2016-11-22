using System;
using System.Linq;
using Qxr.Domain;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Qxr.Repositories.Infrastructures
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly QxrDataContext _dbContext;
        private DbContextTransaction _transaction;

        public EFUnitOfWork()
        {
            _dbContext = DataContextFactory.GetDataContext();
            
        }

        public void BeginUow()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public bool Commit()
        {
            try
            {
                bool flag = _dbContext.SaveChanges() > 0;
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
                return flag;
            }
            catch (DbEntityValidationException dvex)
            {
                var errors = dvex.EntityValidationErrors.SelectMany(m => m.ValidationErrors).Select(m => m.ErrorMessage);
                Console.WriteLine(string.Join(Environment.NewLine, errors));
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                ResetDataContent();
                return false;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                ResetDataContent();
                //Logger.Error("Unit of work:", ex);
                Console.WriteLine("Error");
                return false;
            }
            finally
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        private void ResetDataContent()
        {
            //TODO:只有再次初始化Repository和UnitOfWork时才有效，加入Commit失败之后还要记录日志到数据库，这个场景会报错
            DataContextFactory.ResetDataContent();
        }
    }
}
