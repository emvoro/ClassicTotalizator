using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync() ?? new List<Account>();
        }

        public async Task<Account> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> Add(Account account)
        {
            if (account == null)
                return false;
            
            if (await GetByEmail(account.Email) != null)
                return false;

            account.Id = Guid.NewGuid();
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