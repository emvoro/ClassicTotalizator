using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class SportRepository : ISportRepository
    {
        private readonly DbSet<Sport> _set;

        private readonly DatabaseContext _context;

        public SportRepository(DatabaseContext context)
        {
            _context = context;
            _set = context.Set<Sport>();
        }

        public async Task<Sport> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<Sport>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task AddAsync(Sport obj)
        {
            await _set.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(int id)
        {
            var sport = await GetByIdAsync(id);
            
            await RemoveAsync(sport);
        }

        public async Task RemoveAsync(Sport obj)
        {
            _set.Remove(obj);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sport obj)
        {
            _set.Update(obj);

            await _context.SaveChangesAsync();
        }
    }
}