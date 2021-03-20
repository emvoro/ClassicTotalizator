using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AccountDTO>> GetAll();
        Task<AccountDTO> GetById(Guid id);
        Task<AccountDTO> GetByEmail(string email);
        Task<bool> Add(AccountDTO registeredAcc);
        Task<bool> Remove(Guid id);
    }
}