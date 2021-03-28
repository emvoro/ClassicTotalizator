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

        public AuthService(IAccountService accountService, 
            IHashGenerator hashGenerator)
        {
            _accountService = accountService;
            _hashGenerator = hashGenerator;
        }

        public async Task<string> AdminLoginAsync(AccountLoginDTO accountLoginDTO)
        {
            if (accountLoginDTO == null)
                throw new ArgumentNullException(nameof(accountLoginDTO));
            
            var accountFromBase = await _accountService.GetByEmailAsync(accountLoginDTO.Login);

            if (accountFromBase == null || accountFromBase.AccountType != Roles.Admin)
                return null;
            
            if (!CheckPasswords(accountFromBase.PasswordHash, accountLoginDTO.Password))
                return null;
            
            return new JwtGenerator().GenerateJwt(accountFromBase, SecurityKey);
        }

        public async Task<string> LoginAsync(AccountLoginDTO accountLoginDTO)
        {
            if (accountLoginDTO == null)
                throw new ArgumentNullException(nameof(accountLoginDTO));
            
            if (string.IsNullOrEmpty(accountLoginDTO.Login) || string.IsNullOrEmpty(accountLoginDTO.Password))
                return null;

            var accFromBase = await _accountService.GetByEmailAsync(accountLoginDTO.Login);

            if (accFromBase == null)
                return null;
            
            if (!CheckPasswords(accFromBase.PasswordHash, accountLoginDTO.Password))
                return null;
            
            return new JwtGenerator().GenerateJwt(accFromBase, SecurityKey);
        }

        public async Task<string> RegisterAsync(AccountRegisterDTO accountRegisterDTO)
        {
            if (string.IsNullOrEmpty(accountRegisterDTO.Email) || string.IsNullOrEmpty(accountRegisterDTO.Password))
                return null;
            
            var age = DateTime.UtcNow.Year + (double) DateTime.UtcNow.Month / 12 -
                        (accountRegisterDTO.DOB.Year + (double) accountRegisterDTO.DOB.Month / 12);

            if (age < 18) return null;

            var newId = Guid.NewGuid();
            var accountForRegister = new AccountDTO
            {
                Id = newId,
                AccountType = Roles.User,
                PasswordHash = _hashGenerator.GenerateHash(accountRegisterDTO.Password),
                Email = accountRegisterDTO.Email,
                Username = accountRegisterDTO.Username,
                DOB = accountRegisterDTO.DOB,
                AccountCreationTime = accountRegisterDTO.AccountCreationTime,
                AvatarLink =$"https://avatars.dicebear.com/api/human/{newId}.png"
            };

            if (await _accountService.AddAsync(accountForRegister))
                return new JwtGenerator().GenerateJwt(accountForRegister, SecurityKey);
            
            return null;
        }

        private bool CheckPasswords(string passwordHash, string passwordFromLogin)
        {
            return _hashGenerator.GenerateHash(passwordFromLogin) == passwordHash;
        }
    }
}
