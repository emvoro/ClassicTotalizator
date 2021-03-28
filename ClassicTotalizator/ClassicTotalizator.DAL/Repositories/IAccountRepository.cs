using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByEmailAsync(string email);

        Task<Account> GetAccountByUsernameAsync(string username);
    }
}