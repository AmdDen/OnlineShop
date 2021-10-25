using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Dal
{
    public interface IRepository
    {
        Task<TEntity> Find<TEntity>(int id) where TEntity: BaseEntity;

        Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: BaseEntity;

        Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity: BaseEntity;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity: BaseEntity;
        IQueryable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: BaseEntity;

        void Add<TEntity>(TEntity entity) where TEntity: BaseEntity;

        TEntity Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task Save();
    }
}
