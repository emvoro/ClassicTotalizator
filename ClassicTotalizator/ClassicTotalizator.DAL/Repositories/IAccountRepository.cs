using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByEmail(string email);

        Task<Account> GetAccountByUsername(string username);
    }
}