using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _set.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Account> GetAccountByUsername(string username)
        {
            return await _set.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}