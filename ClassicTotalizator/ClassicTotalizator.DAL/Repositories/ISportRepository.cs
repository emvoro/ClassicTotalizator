using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface ISportRepository
    {
        Task<Sport> GetByIdAsync(int id);

        Task<IEnumerable<Sport>> GetAllAsync();

        Task AddAsync(Sport obj);

        Task RemoveByIdAsync(int id);
        
        Task RemoveAsync(Sport obj);

        Task UpdateAsync(Sport obj);
    }
}