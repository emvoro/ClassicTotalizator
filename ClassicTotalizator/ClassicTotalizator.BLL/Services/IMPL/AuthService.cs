using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Generators.IMPL;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AuthService : IAuthService
    {
        public string SecurityKey { get; set; }
       
        private readonly IUserService _userService;
        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> LoginAsync(AccountLoginDTO loginDto)
        {
            if (loginDto.Login == null || loginDto.Password == null)
                return null;
            var accountForLogin = AccountMapper.ToAccount(loginDto);

            var loginnedAccount = await _userService.GetByEmail(loginDto.Login);
            if (loginnedAccount != null)
                return new JwtGenerator()
                    .GenerateJwt(loginnedAccount, SecurityKey);
            return null;
        }

        public async Task<string> RegisterAsync(AccountRegisterDTO registerDto)
        {
            if (registerDto.Email == null || registerDto.Password == null || registerDto.DOB == null)
                return null;
            var accountForRegister = AccountMapper.ToAccount(registerDto);
            accountForRegister.AccountType = "USER";

            if (await _userService.Add(accountForRegister))
                return new JwtGenerator()
                    .GenerateJwt(accountForRegister, SecurityKey);
            return null;
        }
    }
}
