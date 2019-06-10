using Ilkyar.Contracts.Entities;
using Ilkyar.Contracts.Repositories;
using Ilkyar.Contracts.UnitOfWork;
using Ilkyar.Data;
using Ilkyar.Infrastructure.Repositories;
using System;
using System.Data.Entity.Infrastructure;

namespace Ilkyar.Infrastructure.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IlkyarEntities _dbContext;

        public EFUnitOfWork()
        {
            _dbContext = new IlkyarEntities();
        }

        #region IUnitOfWork Members
        public IRepository<T> GetRepository<T>() where T : ModelBase
        {
            return new EFRepository<T>(_dbContext);
        }
        public DbRawSqlQuery<T> SQLQuery<T>(string sql)
        {
            return _dbContext.Database.SqlQuery<T>(sql);
        }
        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region IDisposable Members
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
