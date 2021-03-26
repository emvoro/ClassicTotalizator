using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> Set;
        protected readonly DatabaseContext Context;

        public Repository(DatabaseContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }
        
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task AddAsync(T obj)
        {
            await Set.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var obj = await GetByIdAsync(id);
            
            await RemoveAsync(obj);
        }

        public async Task RemoveAsync(T obj)
        {
            Set.Remove(obj);
            
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            Set.Update(obj);
            
            await Context.SaveChangesAsync();
        }
    }
}