using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.IMPL;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class BetServiceTests
    {
        private IBetService _betService;
        
        private readonly Mock<IBetRepository> _mockBetRepository = new Mock<IBetRepository>();

        private readonly Mock<IRepository<Wallet>> _mockWalletRepository = new Mock<IRepository<Wallet>>();

        private readonly Mock<IRepository<BetPool>> _mockBetpoolRepository = new Mock<IRepository<BetPool>>();

        private readonly Mock<IRepository<Participant>> _mockParticipantRepository = new Mock<IRepository<Participant>>();

        private readonly Mock<IRepository<Event>> _mockEventRepository = new Mock<IRepository<Event>>();

        [Fact]
        public async Task GetAllEventBets_Returns_EmptyList_If_RepositoryIsEmpty()
        {
            _mockBetRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Bet>());

            _betService = new BetService(_mockBetRepository.Object, null, null, null, null);

            var bets = await _betService.GetAllEventBetsAsync();
            
            Assert.Empty(bets);
        }

        [Fact]
        public async Task GetAllEventBets_Returns_ListWithTwoObjects()
        {
            var id = Guid.NewGuid();

            _mockBetRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Bet>
            {
                new Bet
                {
                    Event_Id = id
                },
                new Bet
                {
                    Event_Id = id
                }
            });
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Participant_Id1 = id, Participant_Id2 = id
            });
            _mockParticipantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());

            _betService = new BetService(_mockBetRepository.Object, null, null, _mockEventRepository.Object,
                _mockParticipantRepository.Object);

            var bets = await _betService.GetAllEventBetsAsync();
            
            Assert.Equal(2, bets.Count());
        }

        [Fact]
        public async Task GetBetsByAccIdAsync_ReturnsEmptyList()
        {
            var id = Guid.NewGuid();
            _mockBetRepository.Setup(x => x.GetBetsByAccountIdAsync(id)).ReturnsAsync(new List<Bet>());

            _betService = new BetService(_mockBetRepository.Object, null, null, null, null);

            var bets = await _betService.GetBetsByAccIdAsync(id);
            
            Assert.Empty(bets);
        }
        
        [Fact]
        public async Task GetBetsByAccIdAsync_ReturnsNull_If_GuidIsEmpty()
        {
            _betService = new BetService(null, null, null, null, null);

            var bets = await _betService.GetBetsByAccIdAsync(Guid.Empty);
            
            Assert.Null(bets);
        }

        [Fact]
        public async Task GetBetsByAccIdAsync_ReturnsListWithTwoObjects()
        {
            var id = Guid.NewGuid();

            _mockBetRepository.Setup(x => x.GetBetsByAccountIdAsync(id)).ReturnsAsync(new List<Bet>
            {
                new Bet
                {
                    Event_Id = id
                },
                new Bet
                {
                    Event_Id = id
                }
            });
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Participant_Id1 = id, Participant_Id2 = id
            });
            _mockParticipantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());

            _betService = new BetService(_mockBetRepository.Object, null, null, _mockEventRepository.Object,
                _mockParticipantRepository.Object);

            var bets = await _betService.GetBetsByAccIdAsync(id);
            
            Assert.Equal(2, bets.Count());
        }

        [Fact]
        public async Task AddBetAsync_Throws_ArgumentNullException_If_ParameterIsNull()
        {
            _betService = new BetService(null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _betService.AddBetAsync(null, Guid.Empty));
        }

        [Theory]
        [MemberData(nameof(NewBets))]
        public async Task AddBetAsync_ReturnFalse_If_ModelOrId_Invalid(BetNewDTO betDto, Guid id)
        {
            _betService = new BetService(null, null, null, null, null);

            Assert.False(await _betService.AddBetAsync(betDto, id));
        }

        [Fact]
        public async Task AddBetAsync_ReturnFalse_If_EventNotFound()
        {
            var id = Guid.NewGuid();
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Event)null);
            
            _betService = new BetService(null, null, null, _mockEventRepository.Object, null);

            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = Guid.NewGuid()}, id));
        }
        
        [Theory]
        [MemberData(nameof(Events))]
        public async Task AddBetAsync_ReturnFalse_If_EventEnded_Or_EventStarted(Event @event)
        {
            var id = Guid.NewGuid();
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);
            
            _betService = new BetService(null, null, null, _mockEventRepository.Object, null);

            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = Guid.NewGuid()}, id));
        }

        [Fact]
        public async Task AddBetAsync_ReturnFalse_If_BetPoolNotFound()
        {
            var id = Guid.NewGuid();
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Id = id, IsEnded = false, StartTime = new DateTimeOffset(2022, 9, 5, 4, 4, 4, TimeSpan.Zero)
            });
            
            _mockBetpoolRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((BetPool) null);
            
            _betService = new BetService(null, null, _mockBetpoolRepository.Object, _mockEventRepository.Object, null);
            
            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = id, Amount = 1, Choice = "W1"}, id));
        }
        
        [Theory]
        [MemberData(nameof(Wallets))]
        public async Task AddBetAsync_ReturnFalse_If_WalletNotFound_OrLessAmount(Wallet wallet, decimal amount)
        {
            var id = Guid.NewGuid();
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Id = id, IsEnded = false, StartTime = new DateTimeOffset(2022, 9, 5, 4, 4, 4, TimeSpan.Zero)
            });
            
            _mockBetpoolRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new BetPool{TotalAmount = 0});

            _mockWalletRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);
            
            _betService = new BetService(null, _mockWalletRepository.Object, _mockBetpoolRepository.Object,
                _mockEventRepository.Object, null);
            
            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = id, Amount = amount, Choice = "W1"}, id));
        }

        [Fact]
        public async Task AddBetAsync_ReturnTrue_If_AllParameters_Is_Valid()
        {
            var id = Guid.NewGuid();
            var betPool = new BetPool
                {TotalAmount = 0, Bets = new List<Bet>(), Event_Id = id};
            var wallet = new Wallet {Amount = 1000};
            var @event = new Event
            {
                Id = id, IsEnded = false, StartTime = new DateTimeOffset(2022, 9, 5, 4, 4, 4, TimeSpan.Zero)
            };
            var bet = new BetNewDTO
            {
                Amount = 1,
                Choice = "W1",
                Event_Id = id
            };
            
            _mockEventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);
            
            _mockBetpoolRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(betPool);
            _mockBetpoolRepository.Setup(x => x.UpdateAsync(betPool)).Returns(Task.CompletedTask);
            
            _mockWalletRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);
            _mockWalletRepository.Setup(x => x.UpdateAsync(wallet)).Returns(Task.CompletedTask);

            _mockBetRepository.Setup(x => x.AddAsync(BetMapper.Map(bet))).Returns(Task.CompletedTask);

            _betService = new BetService(_mockBetRepository.Object, _mockWalletRepository.Object, _mockBetpoolRepository.Object,
                _mockEventRepository.Object, null);
            
            Assert.True(await _betService.AddBetAsync(bet, id));
        }

        public static IEnumerable<object[]> Wallets()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Wallet
                    {
                        Amount = 100
                    },
                    120
                },
                new object[]
                {
                    null,
                    100
                }
            };
        }
        
        public static IEnumerable<object[]> Events()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Event
                    {
                        IsEnded = false,
                        StartTime = DateTimeOffset.Now
                    }
                },
                new object[]
                {
                    new Event
                    {
                        IsEnded = true,
                        StartTime = DateTimeOffset.Now
                    }
                },
                new object[]
                {
                    new Event
                    {
                        IsEnded = true,
                        StartTime = DateTimeOffset.UtcNow
                    }
                },
            };
        }

        public static IEnumerable<object[]> NewBets()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new BetNewDTO
                    {
                        Event_Id = Guid.NewGuid(),
                        Amount = -1,
                        Choice = "W1"
                    },
                    Guid.NewGuid()
                },
                new object[]
                {
                    new BetNewDTO
                    {
                        Event_Id = Guid.NewGuid(),
                        Amount = 1,
                        Choice = ""
                    },
                    Guid.NewGuid()
                },
                new object[]
                {
                    new BetNewDTO
                    {
                        Event_Id = Guid.Empty,
                        Amount = 1,
                        Choice = "W1"
                    },
                    Guid.NewGuid()
                },
                new object[]
                {
                    new BetNewDTO
                    {
                        Amount = 1,
                        Choice = "W1"
                    },
                    Guid.Empty
                },
            };
        }
    }
}