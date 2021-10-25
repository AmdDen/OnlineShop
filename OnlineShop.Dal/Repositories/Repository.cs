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
    public class Repository : IRepository
    {
        private readonly OnlineShopDbContext _context;

        public Repository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> Find<T>(int id) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Find<T>(Expression<Func<T, bool>> predicate)  where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> FindAll<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _context.Set<T>().OrderByDescending(x => x.Id);
        }

        public async Task<T> GetByIdWithInclude<T>(int id, params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public T Update<T>(T entity) where T : BaseEntity
        {
            return _context.Set<T>().Update(entity).Entity;
        }

        public IQueryable<T> IncludeProperties<T>(params Expression<Func<T, object>>[] includeProperties) where T : BaseEntity
        {
            IQueryable<T> entities = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
