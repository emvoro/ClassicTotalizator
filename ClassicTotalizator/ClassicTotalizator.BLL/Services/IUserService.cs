using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IUserService
    {
        Task<AccountDTO> GetByEmail(string email);
        
        Task<bool> Add(AccountDTO registeredAcc);
        
        Task<bool> Remove(Guid id);
    }
}