using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetById(Guid id);
        Task<Account> GetByEmail(string email);
        Task<bool> Add(Account account);
        Task<bool> Remove(Guid id);
    }
}