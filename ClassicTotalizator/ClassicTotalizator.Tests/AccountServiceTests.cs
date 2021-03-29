using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class AccountServiceTests
    {
        private IAccountService _accountService;
        
        private readonly Mock<IAccountRepository> _mockAccountRepository = new Mock<IAccountRepository>();

        private readonly Mock<IRepository<Wallet>> _mockWalletRepository = new Mock<IRepository<Wallet>>();

        [Fact]
        public async Task GetAllAccountsAsync_ReturnEmptyList_IfRepositoryIsEmpty()
        {
            _mockAccountRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Account>());

            _mockWalletRepository.Setup(x => x.GetByIdAsync(Guid.NewGuid())).ReturnsAsync((Wallet) null);

            _accountService = new AccountService(_mockAccountRepository.Object, _mockWalletRepository.Object);

            var accounts = await _accountService.GetAllAccountsAsync();
            
            Assert.Empty(accounts);
        }

        [Fact]
        public async Task GetAllAccountsAsync_ReturnListWithTwoElements_Because_RepositoryHaveTwoAccounts()
        {
            _mockAccountRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Account>
            {
                new Account(),
                new Account()
            });

            _mockWalletRepository.Setup(x => x.GetByIdAsync(Guid.NewGuid())).ReturnsAsync((Wallet) null);

            _accountService = new AccountService(_mockAccountRepository.Object, _mockWalletRepository.Object);

            var accounts = await _accountService.GetAllAccountsAsync();
            
            Assert.Equal(2, accounts.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnNull_IfGuidIsEmpty()
        {
            _accountService = new AccountService(null, null);
            
            Assert.Null(await _accountService.GetByIdAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnNull_If_RepositoryDoNotHaveElementWith_ThisGuid()
        {
            var id = Guid.NewGuid();
            _mockAccountRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Account) null);

            _accountService = new AccountService(_mockAccountRepository.Object, null);
            
            Assert.Null(await _accountService.GetByIdAsync(id));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnElement_If_RepositoryHaveElementWith_ThisGuid()
        {
            var id = Guid.NewGuid();
            _mockAccountRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Account{Id = id});

            _accountService = new AccountService(_mockAccountRepository.Object, null);

            var account = await _accountService.GetByIdAsync(id);
            
            Assert.NotNull(account);
            Assert.Equal(id, account.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetByEmailAsync_ReturnNullIf_EmailEmptyOrNull(string email)
        {
            _accountService = new AccountService(null, null);
            
            Assert.Null(await _accountService.GetByEmailAsync(email));
        }
        
        [Fact]
        public async Task GetByEmailAsync_ReturnElement_If_RepositoryHaveElementWith_ThisEmail()
        {
            var email = "asd@asd.asd";
            _mockAccountRepository.Setup(x => x.GetAccountByEmailAsync(email))
                .ReturnsAsync(new Account {Email = email});
            
            _accountService = new AccountService(_mockAccountRepository.Object, null);

            var account = await _accountService.GetByEmailAsync(email);
            
            Assert.NotNull(account);
            Assert.Equal(email, account.Email);
        }
        
        [Fact]
        public async Task GetByEmailAsync_ReturnNull_If_RepositoryDoNotHaveElementWith_ThisEmail()
        {
            var email = "asd@asd.asd";
            _mockAccountRepository.Setup(x => x.GetAccountByEmailAsync(email))
                .ReturnsAsync((Account) null);
            
            _accountService = new AccountService(_mockAccountRepository.Object, null);

            var account = await _accountService.GetByEmailAsync(email);
            
            Assert.Null(account);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetByUsernameAsync_ReturnNullIf_UsernameEmptyOrNull(string username)
        {
            _accountService = new AccountService(null, null);
            
            Assert.Null(await _accountService.GetByUsernameAsync(username));
        }
        
        [Fact]
        public async Task GetByUsernameAsync_ReturnElement_If_RepositoryHaveElementWith_ThisUsername()
        {
            var username = "username";
            _mockAccountRepository.Setup(x => x.GetAccountByUsernameAsync(username))
                .ReturnsAsync(new Account {Username = username});
            
            _accountService = new AccountService(_mockAccountRepository.Object, null);

            var account = await _accountService.GetByUsernameAsync(username);
            
            Assert.NotNull(account);
            Assert.Equal(username, account.Username);
        }
        
        [Fact]
        public async Task GetByUsernameAsync_ReturnNull_If_RepositoryDoNotHaveElementWith_ThisEmail()
        {
            var username = "username";
            _mockAccountRepository.Setup(x => x.GetAccountByUsernameAsync(username))
                .ReturnsAsync((Account) null);
            
            _accountService = new AccountService(_mockAccountRepository.Object, null);

            var account = await _accountService.GetByUsernameAsync(username);
            
            Assert.Null(account);
        }

        [Fact]
        public async Task AddAsync_ThrowsArgumentNullException_IfParameterIsNull()
        {
            _accountService = new AccountService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _accountService.AddAsync(null));
        }
        
        [Theory]
        [InlineData("dsfsdf", "")]
        [InlineData("", "")]
        [InlineData("sdfsdf", null)]
        [InlineData(null, null)]
        [InlineData(null, "")]
        public async Task AddAsync_ReturnsFalse_If_EmailEmptyOrNullInParameter(string username, string email)
        {
            var account = new AccountDTO
            {
                Email = email,
                Username = username
            };
            
            _accountService = new AccountService(null, null);

            Assert.False(await _accountService.AddAsync(account));
        }

        [Fact]
        public async Task AddAsync_ReturnsTrue_If_ParameterIsValid_InSystemNotFound_ThisEmailAndUsernames()
        {
            var username = "username";
            var email = "email@email.em";
            var account = new AccountDTO
            {
                Username = username,
                Email = email
            };

            _mockAccountRepository.Setup(x => x.GetAccountByEmailAsync(email)).ReturnsAsync((Account) null);
            _mockAccountRepository.Setup(x => x.GetAccountByUsernameAsync(username)).ReturnsAsync((Account) null);

            _accountService = new AccountService(_mockAccountRepository.Object, null);
            
            Assert.True(await _accountService.AddAsync(account));
        }
    }
}