using Ilkyar.Contracts.Entities;
using Ilkyar.Contracts.Repositories;
using System;
using System.Data.Entity.Infrastructure;

namespace Ilkyar.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : ModelBase;
        DbRawSqlQuery<T> SQLQuery<T>(string sql);
        void SaveChanges();
    }
}
