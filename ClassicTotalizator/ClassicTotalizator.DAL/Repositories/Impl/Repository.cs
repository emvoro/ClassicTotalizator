using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _set;
        protected readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<bool> AddAsync(T obj)
        {
            var result = await _set.AddAsync(obj);
            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            var obj = await GetByIdAsync(id);
            
            return await RemoveAsync(obj);
        }

        public async Task<bool> RemoveAsync(T obj)
        {
            var result = _set.Remove(obj);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            var result = _set.Update(obj);
            if(result.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}