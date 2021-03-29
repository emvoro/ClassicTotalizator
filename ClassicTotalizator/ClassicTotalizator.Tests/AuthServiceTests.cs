using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Generators;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class AuthServiceTests
    {
        private IAuthService _authService;
        
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();

        private readonly Mock<IHashGenerator> _hashGenerator = new Mock<IHashGenerator>();
        
        private readonly Mock<IJwtGenerator> _jwtGenerator = new Mock<IJwtGenerator>();
        
        [Fact]
        public async Task AdminLoginAsync_ThrowsArgumentNullException_IfParameterIsNull()
        {
            _authService = new AuthService(null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _authService.AdminLoginAsync(null));
        }
        
        [Theory]
        [InlineData("", "sdfsdf")]
        [InlineData("sdsd", "")]
        [InlineData(null, "dsfsd")]
        [InlineData("dsffds", null)]
        [InlineData(null, null)]
        public async Task AdminLoginAsync_ReturnsNull_IfAccountParameterHave_EmptyLoginOrPassword(string login, string password)
        {
            _authService = new AuthService(null, null, null);
            
            Assert.Null(await _authService.AdminLoginAsync(new AccountLoginDTO{Login = login, Password = password}));
        }

        [Fact]
        public async Task AdminLoginAsync_ReturnsNull_IfAccountNotFound()
        {
            var email = "email@email.em";

            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((AccountDTO) null);

            _authService = new AuthService(_accountService.Object, null, null);
            
            Assert.Null(await _authService.AdminLoginAsync(new AccountLoginDTO{Login = email}));
        }

        [Theory]
        [InlineData("dsfsdf")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(Roles.User)]
        public async Task AdminLoginAsync_ReturnsNull_IfAccountFoundBut_NotAdmin(string accountType)
        {
            var email = "email@email.em";
            var accountDto = new AccountDTO
            {
                Email = email,
                AccountType = accountType
            };
            
            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(accountDto);

            _authService = new AuthService(_accountService.Object, null, null);
            
            Assert.Null(await _authService.AdminLoginAsync(new AccountLoginDTO{Login = email}));
        }

        [Fact]
        public async Task AdminLoginAsync_ReturnNull_IfPasswordNotEquals()
        {
            var email = "email@email.em";
            var passwordHash = "wdqefrgtyhutrgefdcvfvg";
            var accountDto = new AccountDTO
            {
                Email = email,
                AccountType = Roles.Admin,
                PasswordHash = passwordHash
            };
            var password = "sadassda";
            var loginAcc = new AccountLoginDTO
            {
                Login = email,
                Password = password
            };

            _hashGenerator.Setup(x => x.GenerateHash(password)).Returns(password);
            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(accountDto);

            _authService = new AuthService(_accountService.Object, _hashGenerator.Object, null);
            
            Assert.Null(await _authService.AdminLoginAsync(loginAcc));
        }
        
        [Fact]
        public async Task AdminLoginAsync_ReturnJwt_IfModelIsValid_AndModelExistInRepository()
        {
            var email = "email@email.em";
            var passwordHash = "wdqefrgtyhutrgefdcvfvg";
            var accountDto = new AccountDTO
            {
                Email = email,
                AccountType = Roles.Admin,
                PasswordHash = passwordHash
            };
            var password = "sadassda";
            var loginAcc = new AccountLoginDTO
            {
                Login = email,
                Password = password
            };
            var jwt = "kjhgfdfghj";
            var securityKey = "fddsfsdfsdfdsfsadfgh";

            _hashGenerator.Setup(x => x.GenerateHash(password)).Returns(passwordHash);
            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(accountDto);
            _jwtGenerator.Setup(x => x.GenerateJwt(accountDto, securityKey)).Returns(jwt);

            _authService = new AuthService(_accountService.Object, _hashGenerator.Object, _jwtGenerator.Object);
            _authService.SecurityKey = securityKey;

            var jwtReturned = await _authService.AdminLoginAsync(loginAcc);
            
            Assert.Equal(jwt, jwtReturned);
        }

        [Fact]
        public async Task LoginAsync_ThrowsArgumentNullException_IfParameterIsNull()
        {
            _authService = new AuthService(null, null, null);
            
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _authService.LoginAsync(null));
        }
        
        [Theory]
        [InlineData("", "sdfsdf")]
        [InlineData("sdsd", "")]
        [InlineData(null, "dsfsd")]
        [InlineData("dsffds", null)]
        [InlineData(null, null)]
        public async Task LoginAsync_ReturnsNull_IfAccountParameterHave_EmptyLoginOrPassword(string login, string password)
        {
            _authService = new AuthService(null, null, null);
            
            Assert.Null(await _authService.LoginAsync(new AccountLoginDTO{Login = login, Password = password}));
        }
        
        [Fact]
        public async Task LoginAsync_ReturnNull_IfPasswordNotEquals()
        {
            var email = "email@email.em";
            var passwordHash = "wdqefrgtyhutrgefdcvfvg";
            var accountDto = new AccountDTO
            {
                Email = email,
                AccountType = Roles.Admin,
                PasswordHash = passwordHash
            };
            var password = "sadassda";
            var loginAcc = new AccountLoginDTO
            {
                Login = email,
                Password = password
            };

            _hashGenerator.Setup(x => x.GenerateHash(password)).Returns(password);
            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(accountDto);

            _authService = new AuthService(_accountService.Object, _hashGenerator.Object, null);
            
            Assert.Null(await _authService.LoginAsync(loginAcc));
        }
        
        [Fact]
        public async Task LoginAsync_ReturnJwt_IfModelIsValid_AndModelExistInRepository()
        {
            var email = "email@email.em";
            var passwordHash = "wdqefrgtyhutrgefdcvfvg";
            var accountDto = new AccountDTO
            {
                Email = email,
                AccountType = Roles.Admin,
                PasswordHash = passwordHash
            };
            var password = "sadassda";
            var loginAcc = new AccountLoginDTO
            {
                Login = email,
                Password = password
            };
            var jwt = "kjhgfdfghj";
            var securityKey = "fddsfsdfsdfdsfsadfgh";

            _hashGenerator.Setup(x => x.GenerateHash(password)).Returns(passwordHash);
            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(accountDto);
            _jwtGenerator.Setup(x => x.GenerateJwt(accountDto, securityKey)).Returns(jwt);

            _authService = new AuthService(_accountService.Object, _hashGenerator.Object, _jwtGenerator.Object);
            _authService.SecurityKey = securityKey;

            var jwtReturned = await _authService.LoginAsync(loginAcc);
            
            Assert.Equal(jwt, jwtReturned);
        }

        [Fact]
        public async Task RegisterAsync_ThrowsArgumentNullException_IfParameterIsNull()
        {
            _authService = new AuthService(null, null, null);
            
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _authService.RegisterAsync(null));
        }

        [Theory]
        [InlineData("", "dsfsd")]
        [InlineData(null, "dsfsd")]
        [InlineData("asdsd", "")]
        [InlineData("asdds", null)]
        [InlineData(null, null)]
        public async Task RegisterAsync_ReturnsNull_IfModelHaveEmptyFields(string email, string password)
        {
            var account = new AccountRegisterDTO
            {
                Email = email,
                Password = password
            };
            
            _authService = new AuthService(null, null, null);
            
            Assert.Null(await _authService.RegisterAsync(account));
        }

        [Fact]
        public async Task RegisterAsync_ReturnsNull_IfModelHaveAge_LessThenEighteen()
        {
            var account = new AccountRegisterDTO
            {
                Email = "emaildsfsdf",
                Password = "sdfsdfsdfs",
                DOB = new DateTimeOffset(2003, 12, 12, 12, 12, 12, new TimeSpan())
            };
            
            _authService = new AuthService(null, null, null);
            
            Assert.Null(await _authService.RegisterAsync(account));
        }

        [Fact]
        public async Task RegisterAsync_ReturnsNull_IfAccountWithThisEmail_ExistInRepository()
        {
            var email = "gmail@gmail.gm";
            var account = new AccountRegisterDTO
            {
                Email = email,
                Username = "dsfsdfsd",
                Password = "sdfsdfsdfs"
            };

            _accountService.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(new AccountDTO());

            _authService = new AuthService(_accountService.Object, null, null);
            
            Assert.Null(await _authService.RegisterAsync(account));
        }
        
        [Fact]
        public async Task RegisterAsync_ReturnsNull_IfAccountWithThisUsername_ExistInRepository()
        {
            var username = "gmail@gmail.gm";
            var account = new AccountRegisterDTO
            {
                Email = "dsfsdfdsf",
                Password = "sdfsdfsdfs",
                Username = username
            };

            _accountService.Setup(x => x.GetByUsernameAsync(username)).ReturnsAsync(new AccountDTO());

            _authService = new AuthService(_accountService.Object, null, null);
            
            Assert.Null(await _authService.RegisterAsync(account));
        }
    }
}