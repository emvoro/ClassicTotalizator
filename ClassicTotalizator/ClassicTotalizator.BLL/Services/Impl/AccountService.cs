using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.Impl
{

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IRepository<Wallet> _walletRepository;

        public AccountService(IAccountRepository accountRepository,
            IRepository<Wallet> walletRepository)
        {
            _accountRepository = accountRepository;
            _walletRepository = walletRepository;
        }

        public async Task<IEnumerable<AccountForAdminDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            if (accounts == null)
                return null;

            foreach (var account in accounts)
            {
                account.Wallet = await _walletRepository.GetByIdAsync(account.Id);
            }

            return accounts.Select(AccountMapper.MapForAdmin).ToList();
        }

        public async Task<AccountInfoDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var account = await _accountRepository.GetByIdAsync(id);

            return AccountMapper.MapForChatInfo(account);
        }

        public async Task<AccountDTO> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var account = await _accountRepository.GetAccountByEmailAsync(email);

            return AccountMapper.Map(account);
        }

        public async Task<AccountDTO> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var account = await _accountRepository.GetAccountByUsernameAsync(username);

            return AccountMapper.Map(account);
        }

        public async Task<bool> AddAsync(AccountDTO registeredAccount)
        {
            if (registeredAccount == null)
                throw new ArgumentNullException(nameof(registeredAccount));
            if (string.IsNullOrEmpty(registeredAccount.Email))
                return false;

            var account = AccountMapper.Map(registeredAccount);

            if (string.IsNullOrEmpty(account.Username?.Trim()))
                account.Username = account.Email.Split("@")[0];

            account.Wallet = new Wallet
            {
                Account = account,
                Account_Id = account.Id
            };
            account.BetsHistory = new List<Bet>();

            await _accountRepository.AddAsync(account);

            return true;
        }
    }
}