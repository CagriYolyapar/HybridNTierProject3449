using Microsoft.EntityFrameworkCore;
using Project.Dal.ContextClasses;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Repositories.Concretes
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly MyContext _context;

        public BaseRepository(MyContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T originalEntity, T newEntity)
        {
            _context.Set<T>().Entry(originalEntity).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp);
        }
    }
}
