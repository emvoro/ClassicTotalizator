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

        public async Task AddAsync(T obj)
        {
            await _set.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var obj = await GetByIdAsync(id);
            
            await RemoveAsync(obj);
        }

        public async Task RemoveAsync(T obj)
        {
            _set.Remove(obj);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _set.Update(obj);
            
            await _context.SaveChangesAsync();
        }
    }
}