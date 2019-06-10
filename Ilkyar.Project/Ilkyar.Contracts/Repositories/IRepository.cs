using Ilkyar.Contracts.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ilkyar.Contracts.Repositories
{
    public interface IRepository<T> where T : ModelBase
    {
        IQueryable<T> Entities { get; }
        T GetById(object id);
        T Get(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        void SaveChanges();
    }
}
