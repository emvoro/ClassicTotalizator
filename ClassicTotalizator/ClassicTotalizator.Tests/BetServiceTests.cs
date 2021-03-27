using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
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
        
        private readonly Mock<IBetRepository> _repository = new Mock<IBetRepository>();

        private readonly Mock<IRepository<Wallet>> _walletRepository = new Mock<IRepository<Wallet>>();

        private readonly Mock<IRepository<BetPool>> _betpoolRepository = new Mock<IRepository<BetPool>>();

        private readonly Mock<IRepository<Participant>> _participantRepository = new Mock<IRepository<Participant>>();

        private readonly Mock<IRepository<Event>> _eventRepository = new Mock<IRepository<Event>>();

        [Fact]
        public async Task GetAllEventBets_Returns_EmptyList_If_RepositoryIsEmpty()
        {
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Bet>());

            _betService = new BetService(_repository.Object, null, null, null, null);

            var bets = await _betService.GetAllEventBetsAsync();
            
            Assert.Empty(bets);
        }

        [Fact]
        public async Task GetAllEventBets_Returns_ListWithTwoObjects()
        {
            var id = Guid.NewGuid();

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Bet>
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
            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Participant_Id1 = id, Participant_Id2 = id
            });
            _participantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());

            _betService = new BetService(_repository.Object, null, null, _eventRepository.Object,
                _participantRepository.Object);

            var bets = await _betService.GetAllEventBetsAsync();
            
            Assert.Equal(2, bets.Count());
        }

        [Fact]
        public async Task GetBetsByAccIdAsync_ReturnsEmptyList()
        {
            var id = Guid.NewGuid();
            _repository.Setup(x => x.GetBetsByAccountId(id)).ReturnsAsync(new List<Bet>());

            _betService = new BetService(_repository.Object, null, null, null, null);

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

            _repository.Setup(x => x.GetBetsByAccountId(id)).ReturnsAsync(new List<Bet>
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
            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Event
            {
                Participant_Id1 = id, Participant_Id2 = id
            });
            _participantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());

            _betService = new BetService(_repository.Object, null, null, _eventRepository.Object,
                _participantRepository.Object);

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
            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Event)null);
            
            _betService = new BetService(null, null, null, _eventRepository.Object, null);

            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = Guid.NewGuid()}, id));
        }
        
        [Theory]
        [MemberData(nameof(Events))]
        public async Task AddBetAsync_ReturnFalse_If_EventEnded_Or_EventStarted(Event @event)
        {
            var id = Guid.NewGuid();
            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);
            
            _betService = new BetService(null, null, null, _eventRepository.Object, null);

            Assert.False(await _betService.AddBetAsync(new BetNewDTO{Event_Id = Guid.NewGuid()}, id));
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