using AutoMapper;
using ClassicTotalizator.BLL.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AuthService : IAuthService
    {

        public AuthService()
        {

        }
        public Task<string> LoginAsync(AccountLoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterAsync(AccountRegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync(string jwt)
        {
            throw new NotImplementedException();
        }
        private readonly UserManager
        private readonly IMapper _mapper;
    }
}
