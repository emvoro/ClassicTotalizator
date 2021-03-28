using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountForAdminDTO>> GetAllAccountsAsync();
        
        Task<AccountDTO> GetByEmailAsync(string email);

        Task<AccountDTO> GetByUsernameAsync(string username);

        Task<bool> AddAsync(AccountDTO registeredAcc);

        Task<AccountInfoDTO> GetByIdAsync(Guid id);
    }
}