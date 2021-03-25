using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _set;
        private readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        
        public async Task<T> GetById(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<bool> Add(T obj)
        {
            var result = await _set.AddAsync(obj);
            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveById(Guid id)
        {
            var obj = await GetById(id);
            
            return await Remove(obj);
        }

        public async Task<bool> Remove(T obj)
        {
            var result = _set.Remove(obj);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Update(T obj)
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