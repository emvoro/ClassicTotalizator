using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountForAdminDTO>> GetAllAccounts();
        
        Task<AccountDTO> GetByEmail(string email);

        Task<AccountDTO> GetByUsername(string username);

        Task<bool> Add(AccountDTO registeredAcc);

        Task<AccountInfoDTO> GetById(Guid id);
    }
}