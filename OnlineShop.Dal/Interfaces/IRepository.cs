using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Dal
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Find(int id);

        Task<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdWithInclude(int id, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        Task Save();
    }
}
