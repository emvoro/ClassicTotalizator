using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AuthService : IAuthService
    {
        public Task<string> LoginAsync(AccountLoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(AccountRegisterDTO registerDto)
        {
            //

            //

            //

            return 
        }

        public Task<bool> LogoutAsync(string jwt)
        {
            throw new NotImplementedException();
        }
    }
}
