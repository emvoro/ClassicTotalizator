using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task<bool> Add(T obj);

        Task<bool> RemoveById(Guid id);
        
        Task<bool> Remove(T obj);

        Task<bool> Update(T obj);
    }
}