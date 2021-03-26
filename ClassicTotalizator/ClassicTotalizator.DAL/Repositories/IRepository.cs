using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> AddAsync(T obj);

        Task<bool> RemoveByIdAsync(Guid id);
        
        Task<bool> RemoveAsync(T obj);

        Task<bool> UpdateAsync(T obj);
    }
}