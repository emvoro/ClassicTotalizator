using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _context;

        public AccountService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountForAdminDTO>> GetAllAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            foreach (var account in accounts)
            {
                account.Wallet = await _context.Wallets.FindAsync(account.Id);
            }

            return accounts.Select(AccountMapper.MapForAdmin).ToList();
        }

        public async Task<AccountDTO> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
            
            return AccountMapper.Map(account);
        }

        public async Task<bool> Add(AccountDTO registeredAcc)
        {
            if (registeredAcc == null)
                throw new ArgumentNullException(nameof(registeredAcc));
            if (await GetByEmail(registeredAcc.Email) != null)
                return false;

            var account = AccountMapper.Map(registeredAcc);

            if (account.Username == null)
                account.Username = account.Email.Split("@")[0];
            
            account.Wallet = new Wallet
            {
                Account = account,
                Account_Id = account.Id,
                TransactionsHistory = new List<Transaction>()   
            };
            account.BetsHistory = new List<Bet>();

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}