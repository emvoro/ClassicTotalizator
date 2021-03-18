using AutoMapper;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AuthService : IAuthService
    {

        public AuthService(IMapper mapper,
            UserManager<Account> accManager)
        {
            _mapper = mapper;
            _accManager = accManager;
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
        private readonly UserManager<Account> _accManager;
        private readonly IMapper _mapper;
    }
}
