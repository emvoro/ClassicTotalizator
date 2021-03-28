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

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await Set.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await Set.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}