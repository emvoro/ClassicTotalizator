using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class WalletServiceTests
    {
        private IWalletService _service;
        
        private readonly Mock<IRepository<Wallet>> _mockRepository;

        private readonly Mock<ITransactionRepository> _mockTransactionRepository;

        public WalletServiceTests()
        {
            _mockRepository = new Mock<IRepository<Wallet>>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
        }

        [Fact]
        public async void Transaction_ThrowArgumentNullException_IfTransactionDtoIsNull()
        {
            _service = new WalletService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.Transaction(Guid.Empty, null));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        [InlineData(400)]
        [InlineData(10000)]
        [InlineData(1)]
        public async void Transaction_AddToWalletAmount_If_TransactionValidDeposit(decimal amount)
        {
            var id = Guid.NewGuid();
            var wallet = new Wallet
            {
                Amount = 0,
                Account_Id = id
            };
            var transaction = new TransactionDTO()
            {
                Amount = amount,
                Type = "deposit"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);

            _mockTransactionRepository.Setup(x => x.AddAsync(TransactionMapper.Map(transaction))).Returns(Task.CompletedTask);

            _service = new WalletService(_mockRepository.Object, _mockTransactionRepository.Object);

            var result = await _service.Transaction(id, transaction);
            
            Assert.Equal(amount ,result.Amount);
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        [InlineData(400)]
        [InlineData(10000)]
        [InlineData(1)]
        public async void Transaction_RemoveFromWalletAmount_If_TransactionValidWithdraw(decimal amount)
        {
            var id = Guid.NewGuid();
            var wallet = new Wallet
            {
                Amount = amount,
                Account_Id = id
            };
            var transaction = new TransactionDTO()
            {
                Amount = amount,
                Type = "withdraw"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);

            _mockTransactionRepository.Setup(x => x.AddAsync(TransactionMapper.Map(transaction))).Returns(Task.CompletedTask);

            _service = new WalletService(_mockRepository.Object, _mockTransactionRepository.Object);

            var result = await _service.Transaction(id, transaction);
            
            Assert.Equal(0 ,result.Amount);
        }

        [Theory]
        [InlineData("asd")]
        [InlineData("Deposit")]
        [InlineData("Withdraw")]
        [InlineData("wqretyyjhgfdwdefrgthy")]
        [InlineData("123r45ghyhtgrfd")]
        [InlineData("")]
        [InlineData(null)]
        public async void Transaction_ReturnNull_If_TypeInvalid(string type)
        {
            var id = Guid.NewGuid();
            var transaction = new TransactionDTO()
            {
                Amount = 0,
                Type = type
            };

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Wallet());

            _mockTransactionRepository.Setup(x => x.AddAsync(TransactionMapper.Map(transaction))).Returns(Task.CompletedTask);

            _service = new WalletService(_mockRepository.Object, _mockTransactionRepository.Object);

            var result = await _service.Transaction(id, transaction);
            
            Assert.Null(result);
        }

        [Theory]
        [InlineData(100, null)]
        [InlineData(0, null)]
        [InlineData(-12, null)]
        [InlineData(1, "")]
        [InlineData(0, "")]
        [InlineData(-1, "")]
        public async void Transaction_ReturnNull_IfTransactionInvalid(decimal amount, string type)
        {
            var id = Guid.NewGuid();
            var transaction = new TransactionDTO()
            {
                Amount = amount,
                Type = type
            };
            
            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Wallet());

            _mockTransactionRepository.Setup(x => x.AddAsync(TransactionMapper.Map(transaction))).Returns(Task.CompletedTask);

            _service = new WalletService(_mockRepository.Object, _mockTransactionRepository.Object);

            var result = await _service.Transaction(id, transaction);
            
            Assert.Null(result);
        }

        [Fact]
        public async void GetWalletByAccId_ReturnNull_IfIdIsEmpty()
        {
            _service = new WalletService(null, null);
            
            Assert.Null(await _service.GetWalletByAccId(Guid.Empty));
        }

        [Fact]
        public async void GetWalletByAccId_ReturnValidWalletAccount_IfIdEqualToWalletId()
        {
            var id = Guid.NewGuid();
            var wallet = new Wallet
            {
                Amount = 0,
                Account_Id = id
            };

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);

            _service = new WalletService(_mockRepository.Object, null);
            var walletDto = await _service.GetWalletByAccId(id);
            
            Assert.NotNull(walletDto);
            Assert.Equal(wallet.Amount, walletDto.Amount);
        }
        
        [Fact]
        public async void GetWalletByAccId_ReturnNull_If_IdNotEqualToWalletId()
        {
            var id = Guid.NewGuid();

            _mockRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Wallet) null);

            _service = new WalletService(_mockRepository.Object, null);
            var walletDto = await _service.GetWalletByAccId(id);
            
            Assert.Null(walletDto);
        }

        [Fact]
        public async void GetTransactionHistoryByAccId_ReturnsNull_IfGuidEmpty()
        {
            _mockTransactionRepository.Setup(x => x.GetAccountTransaction(Guid.Empty)).ReturnsAsync((IEnumerable<Transaction>) null);

            _service = new WalletService(null, _mockTransactionRepository.Object);
            
            Assert.Null(await _service.GetTransactionHistoryByAccId(Guid.Empty));
        }

        [Fact]
        public async void GetTransactionHistoryByAccId_ReturnsTwoTransaction_If_IdEqualTo_AccountId()
        {
            var id = Guid.NewGuid();
            var transactions = new List<Transaction>
            {
                new() {Wallet_Id = id},
                new () {Wallet_Id = id}
            };

            _mockTransactionRepository.Setup(x => x.GetAccountTransaction(id)).ReturnsAsync(transactions);

            _service = new WalletService(null, _mockTransactionRepository.Object);

            var actual = await _service.GetTransactionHistoryByAccId(id);
            
            Assert.Equal(2, actual.Count());
        }
    }
}