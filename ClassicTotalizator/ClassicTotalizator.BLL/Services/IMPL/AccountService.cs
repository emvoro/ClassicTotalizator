using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        private readonly IRepository<Wallet> _walletRepository;

        public AccountService(IAccountRepository repository, 
            IRepository<Wallet> walletRepository)
        {
            _repository = repository;
            _walletRepository = walletRepository;
        }

        public async Task<IEnumerable<AccountForAdminDTO>> GetAllAccounts()
        {
            var accounts = await _repository.GetAllAsync();
            foreach (var account in accounts)
            {
                account.Wallet = await _walletRepository.GetByIdAsync(account.Id);
            }

            return accounts.Select(AccountMapper.MapForAdmin).ToList();
        }

        public async Task<AccountInfoDTO> GetById(Guid id)
        {
            var account = await _repository.GetByIdAsync(id);

            return AccountMapper.MapForChatInfo(account);
        }

        public async Task<AccountDTO> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            
            var account = await _repository.GetAccountByEmail(email);
            
            return AccountMapper.Map(account);
        }

        public async Task<AccountDTO> GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var account = await _repository.GetAccountByUsername(username);

            return AccountMapper.Map(account);
        }

        public async Task<bool> Add(AccountDTO registeredAccount)
        {
            if (registeredAccount == null)
                throw new ArgumentNullException(nameof(registeredAccount));

            if (await GetByEmail(registeredAccount.Email) != null)
                return false;

            if (await GetByUsername(registeredAccount.Username) != null)
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

            await _repository.AddAsync(account);
            
            return true;
        }
    }
}