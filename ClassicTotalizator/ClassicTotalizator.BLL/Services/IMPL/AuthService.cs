using System;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Generators.IMPL;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Generators;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class AuthService : IAuthService
    {
        public string SecurityKey { get; set; }
       
        private readonly IAccountService _accountService;

        private readonly IHashGenerator _hashGenerator;
        
        public AuthService(IAccountService accountService, IHashGenerator hashGenerator)
        {
            _accountService = accountService;
            _hashGenerator = hashGenerator;
        }

        public async Task<string> LoginAsync(AccountLoginDTO loginDto)
        {
            if (loginDto.Login == null || loginDto.Password == null)
                return null;

            var accFromBase = await _accountService.GetByEmail(loginDto.Login);
            if (accFromBase == null)
                return null;
            
            if (!CheckPasswords(accFromBase.PasswordHash, loginDto.Password))
                return null;
            
            return new JwtGenerator().GenerateJwt(accFromBase, SecurityKey);
        }

        public async Task<string> RegisterAsync(AccountRegisterDTO registerDto)
        {
            if (registerDto.Email == null || registerDto.Password == null)
                return null;

            var accountForRegister = new AccountDTO
            {
                Id = Guid.NewGuid(),
                AccountType = "USER",
                PasswordHash = _hashGenerator.GenerateHash(registerDto.Password),
                Email = registerDto.Email,
                DOB = registerDto.DOB,
                AccountCreationTime = registerDto.AccountCreationTime
            };

            if (await _accountService.Add(accountForRegister))
                return new JwtGenerator()
                    .GenerateJwt(accountForRegister, SecurityKey);
            
            return null;
        }

        private bool CheckPasswords(string passwordHash, string passwordFromLogin)
        {
            return _hashGenerator.GenerateHash(passwordFromLogin) == passwordHash;
        }
    }
}
