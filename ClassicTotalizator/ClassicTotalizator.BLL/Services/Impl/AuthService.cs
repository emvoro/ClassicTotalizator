using System;
using ClassicTotalizator.BLL.Contracts;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Generators;

namespace ClassicTotalizator.BLL.Services.Impl
{
    public class AuthService : IAuthService
    {

        public string SecurityKey { get; set; }

        private readonly IAccountService _accountService;

        private readonly IHashGenerator _hashGenerator;

        private readonly IJwtGenerator _jwtGenerator;

        public AuthService(IAccountService accountService,
            IHashGenerator hashGenerator,
            IJwtGenerator jwtGenerator)
        {
            _accountService = accountService;
            _hashGenerator = hashGenerator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> AdminLoginAsync(AccountLoginDTO accountLoginDto)
        {
            if (accountLoginDto == null)
                throw new ArgumentNullException(nameof(accountLoginDto));

            if (string.IsNullOrEmpty(accountLoginDto.Login) || string.IsNullOrEmpty(accountLoginDto.Password))
                return null;

            var accountFromBase = await _accountService.GetByEmailAsync(accountLoginDto.Login);
            if (accountFromBase == null || accountFromBase.AccountType != Roles.Admin)
                return null;

            if (!CheckPasswords(accountFromBase.PasswordHash, accountLoginDto.Password))
                return null;

            return _jwtGenerator.GenerateJwt(accountFromBase, SecurityKey);
        }

        public async Task<string> LoginAsync(AccountLoginDTO accountLoginDto)
        {
            if (accountLoginDto == null)
                throw new ArgumentNullException(nameof(accountLoginDto));

            if (string.IsNullOrEmpty(accountLoginDto.Login) || string.IsNullOrEmpty(accountLoginDto.Password))
                return null;

            var accFromBase = await _accountService.GetByEmailAsync(accountLoginDto.Login);
            if (accFromBase == null)
                return null;

            if (!CheckPasswords(accFromBase.PasswordHash, accountLoginDto.Password))
                return null;

            return _jwtGenerator.GenerateJwt(accFromBase, SecurityKey);
        }

        public async Task<string> RegisterAsync(AccountRegisterDTO accountRegisterDto)
        {
            if (accountRegisterDto == null)
                throw new ArgumentNullException(nameof(accountRegisterDto));

            if (string.IsNullOrEmpty(accountRegisterDto.Email) || string.IsNullOrEmpty(accountRegisterDto.Password))
                return null;

            var age = DateTime.UtcNow.Year + (double)DateTime.UtcNow.Month / 12 -
                        (accountRegisterDto.DOB.Year + (double)accountRegisterDto.DOB.Month / 12);

            if (age < 18)
                return null;

            if (await _accountService.GetByEmailAsync(accountRegisterDto.Email) != null)
                return null;

            if (await _accountService.GetByUsernameAsync(accountRegisterDto.Username) != null)
                return null;

            var newId = Guid.NewGuid();
            var accountForRegister = new AccountDTO
            {
                Id = newId,
                AccountType = Roles.User,
                PasswordHash = _hashGenerator.GenerateHash(accountRegisterDto.Password),
                Email = accountRegisterDto.Email,
                Username = accountRegisterDto.Username,
                DOB = accountRegisterDto.DOB,
                AccountCreationTime = accountRegisterDto.AccountCreationTime,
                AvatarLink = $"https://avatars.dicebear.com/api/human/{newId}.png"
            };

            if (await _accountService.AddAsync(accountForRegister))
                return _jwtGenerator.GenerateJwt(accountForRegister, SecurityKey);

            return null;
        }

        private bool CheckPasswords(string passwordHash, string passwordFromLogin)
        {
            return _hashGenerator.GenerateHash(passwordFromLogin) == passwordHash;
        }
    }
}