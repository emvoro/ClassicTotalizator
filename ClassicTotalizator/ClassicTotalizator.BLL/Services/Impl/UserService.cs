using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
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

        public async Task<bool> Remove(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}