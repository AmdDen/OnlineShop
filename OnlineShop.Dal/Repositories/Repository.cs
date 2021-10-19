using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Dal.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly OnlineShopDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(OnlineShopDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> Find(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.OrderByDescending(x => x.Id);
        }

        public async Task<T> GetByIdWithInclude(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
