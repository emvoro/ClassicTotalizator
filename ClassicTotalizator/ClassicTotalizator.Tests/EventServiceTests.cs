using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class EventServiceTests
    {
        private IEventService _eventService;

        private readonly Mock<ISportRepository> _sportRepository = new Mock<ISportRepository>();

        private readonly Mock<IRepository<Wallet>> _walletRepository = new Mock<IRepository<Wallet>>();

        private readonly Mock<IBetRepository> _betRepository = new Mock<IBetRepository>();

        private readonly Mock<IRepository<Participant>> _participantRepository = new Mock<IRepository<Participant>>();

        private readonly Mock<IEventRepository> _eventRepository = new Mock<IEventRepository>();

        private readonly Mock<IParameterRepository> _paramRepository = new Mock<IParameterRepository>();

        [Fact]
        public async Task GetById_CheckReturnNullIfGuidEmpty_NullReturned()
        {
            _eventService = new EventService(null, null, null, null, null, null);

            var eventDto = await _eventService.GetById(Guid.Empty);

            Assert.Null(eventDto);
        }

        [Fact]
        public async Task GetById_CheckReturnOfEventForItsId_EventReturned()
        {
            var id = Guid.NewGuid();
            var @event = new Event
            {
                Id = id,
                IsEnded = false,
                BetPool = new BetPool { Event_Id = id, TotalAmount = 0m },
                Margin = 5m,
                Participant1 = null,
                Participant2 = null,
                Participant_Id1 = Guid.NewGuid(),
                Participant_Id2 = Guid.NewGuid(),
                PossibleResults = new string[] { "W1", "X", "W2" },
                Result = null,
                Sport = new Sport { Id = 1, Name = "Test" },
                Sport_Id = 1,
                StartTime = new DateTimeOffset()
            };

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);
            var eventDto = await _eventService.GetById(id);

            Assert.NotNull(eventDto);
            Assert.Equal(eventDto.Id, id);
        }

        [Fact]
        public async Task CreateEventAsync_CheckIfNullArgumentExceptionThrown_ExceptionWasThrown()
        {
            _eventService = new EventService(null, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _eventService.CreateEventAsync(null));
        }

        [Fact]
        public async Task CreateEventAsync_CheckIfNullWasReturned_NullReturned()
        {
            var badEvent1 = new EventRegisterDTO
            {
                Participant_Id1 = Guid.Empty
            };

            var badEvent2 = new EventRegisterDTO
            {
                Participant_Id2 = Guid.Empty
            };

            var badEvent3 = new EventRegisterDTO
            {
                Margin = -3m
            };

            var badEvent4 = new EventRegisterDTO
            {
                StartTime = new DateTimeOffset()
            };

            _eventService = new EventService(null, null, null, null, null, null);

            var createdBadEvent1 = await _eventService.CreateEventAsync(badEvent1);
            var createdBadEvent2 = await _eventService.CreateEventAsync(badEvent2);
            var createdBadEvent3 = await _eventService.CreateEventAsync(badEvent3);
            var createdBadEvent4 = await _eventService.CreateEventAsync(badEvent4);


            Assert.Null(createdBadEvent1);
            Assert.Null(createdBadEvent2);
            Assert.Null(createdBadEvent3);
            Assert.Null(createdBadEvent4);
        }

        [Fact]
        public async Task CreateEventAsync_CheckIfEventWasCreatedNadAddedToDb_EventDtoWasReturned()
        {
            var id = Guid.NewGuid();

            var newEvent = new EventRegisterDTO
            {
                Participant_Id1 = Guid.NewGuid(),
                Participant_Id2 = Guid.NewGuid(),
                Margin = 5,
                SportId = 1,
                StartTime = new DateTime(2021, 10, 5, 11, 30, 0),
                PossibleResults = new string[] { "W1", "W2", "X" }
            };

            _eventRepository.Setup(x => x.AddAsync(EventMapper.Map(newEvent))).Returns(Task.CompletedTask);

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var createdEvent = await _eventService.CreateEventAsync(newEvent);

            Assert.Equal(newEvent.Participant_Id1, createdEvent.Participant_Id1);
        }

        [Fact]
        public async Task EditEventAsync_CheckIfNullWasReturned_NullWasReturned()
        {
            var badEvent1 = new EditedEventDTO
            {
                Margin = -345m
            };

            var badEvent2 = new EditedEventDTO
            {
                StartTime = new DateTime(2002, 10, 14)
            };

            _eventService = new EventService(null, null, null, null, null, null);

            var editedBadEvent1 = await _eventService.EditEventAsync(badEvent1);
            var editedBadEvent2 = await _eventService.EditEventAsync(badEvent2);

            Assert.Null(editedBadEvent1);
            Assert.Null(editedBadEvent2);
        }

        [Fact]
        public async Task EditEventAsync_EventWasCreatedNadAddedToDb_EventDtoWasReturned()
        {
            var id = Guid.NewGuid();

            var newEvent = new EventRegisterDTO
            {
                Participant_Id1 = Guid.NewGuid(),
                Participant_Id2 = Guid.NewGuid(),
                Margin = 5,
                SportId = 1,
                StartTime = new DateTime(2021, 10, 5, 11, 30, 0),
                PossibleResults = new string[] { "W1", "W2", "X" }
            };

            var eventToEdit = new EditedEventDTO
            {
                Margin = 10,
                StartTime = new DateTime(2021, 11, 5, 11, 30, 0)
            };

            _eventRepository.Setup(x => x.AddAsync(EventMapper.Map(newEvent))).Returns(Task.CompletedTask);

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var createdEvent = await _eventService.CreateEventAsync(newEvent);

            eventToEdit.Id = createdEvent.Id;

            _eventRepository.Setup(x => x.GetByIdAsync(createdEvent.Id)).ReturnsAsync(EventMapper.Map(createdEvent));
            _eventRepository.Setup(x => x.UpdateAsync(EventMapper.Map(createdEvent))).Returns(Task.CompletedTask);

            var editedEvent = await _eventService.EditEventAsync(eventToEdit);

            Assert.NotEqual(newEvent.Margin, editedEvent.Margin);
        }

        [Fact]
        public async Task GetEventsAsync_GetCurrentFeed_CheckIfReturnsFullListOfEvents_EventListReturned()
        {
            var id = Guid.NewGuid();
            _eventRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Event>
            {
                new Event
                {
                    Id = id,
                    Participant_Id1 = id,
                    Participant_Id2 =id,
                    Sport_Id =1,
                    IsEnded =true
                },
                new Event
                {
                    Id = id,
                    Participant_Id1 = id,
                    Participant_Id2 =id,
                    Sport_Id =1,
                    IsEnded =true
                }
            });

            _eventRepository.Setup(x => x.GetNotEndedEventsAsync()).ReturnsAsync(new List<Event>
             {
                new Event
                {
                  Id = id,
                    Participant_Id1 = id,
                    Participant_Id2 =id,
                    Sport_Id =1,
                    IsEnded = false
                }
            });

            _participantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());
            _sportRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Sport());
            _betRepository.Setup(x => x.GetBetsByEventIdAsync(id)).ReturnsAsync(new List<Bet>
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
            _paramRepository.Setup(x => x.GetParametersByParticipantIdAsync(id)).ReturnsAsync(new List<Parameter>
            {
                new Parameter
                {
                    Id = id
                }
            });

            _eventService = new EventService(_eventRepository.Object, _participantRepository.Object,
                _sportRepository.Object, _betRepository.Object, null, _paramRepository.Object);

            var events = await _eventService.GetEventsAsync();

            var feed = await _eventService.GetCurrentLineOfEventsAsync();

            var listOfEvents = events.Events.ToList();

            var feedList = feed.Events.ToList();

            Assert.Equal(2, listOfEvents.Count());
            Assert.Single(feedList);
        }

        [Fact]
        public async Task FinishEvent_CheckIfNullArgumentExceptionThrown_ExceptionWasThrown()
        {
            _eventService = new EventService(null, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _eventService.FinishEventAsync(null));
        }

        [Fact]
        public async Task FinishEvent_CheckIfFalseWasReturnedIfNoSuchEventToCloseOrBadEventResult_FalseReturned()
        {
            var id = Guid.NewGuid();

            var finishedEvent = new FinishedEventDTO
            {
                Id = id,
                Result = "F"
            };

            _eventRepository.Setup(x => x.GetByIdAsync(Guid.NewGuid())).ReturnsAsync((Event)null);


            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var finished = await _eventService.FinishEventAsync(finishedEvent);

            Assert.False(finished);
        }

        [Fact]
        public async Task FinishEvent_CheckIfFalseWasReturnedIfBadEventResult_FalseReturned()
        {
            var id = Guid.NewGuid();

            var finishedEvent = new FinishedEventDTO
            {
                Id = id,
                Result = "F"
            };

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(
               new Event
               {
                   Id = id,
                   PossibleResults = new string[] { "W1", "W2" }
               }
               );

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var badEventResult = await _eventService.FinishEventAsync(finishedEvent);

            Assert.False(badEventResult);
        }

        [Fact]
        public async Task FinishEvent_CheckIfFalseWasReturnedIfEventAlreadyEnded_FalseReturned()
        {
            var id = Guid.NewGuid();

            var eventToFinish = new FinishedEventDTO
            {
                Id = id,
                Result = "W1"
            };

            _eventRepository.Setup(x => x.GetByIdAsync(eventToFinish.Id)).ReturnsAsync(new Event
            {
                Id = id,
                IsEnded = true,
                PossibleResults = new string[] { "W1", "W2" }
            });

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var eventAlreadyFinished = await _eventService.FinishEventAsync(eventToFinish);

            Assert.False(eventAlreadyFinished);
        }

        [Fact]
        public async Task FinishEvent_CheckIfTrueWasReturnedIfEventSuccessfullyFinished_TrueReturned()
        {
            var id = Guid.NewGuid();
            var defaultWalletId = Guid.NewGuid();

            var wallets = new List<Wallet>
            {
                new Wallet
                {
                    Account_Id = id,
                    Amount =0
                },
                new Wallet
                {
                    Account_Id =defaultWalletId,
                    Amount = 0
                }
            };

            var eventToFinish = new FinishedEventDTO
            {
                Id = id,
                Result = "W1"
            };
            var @event = new Event
            {
                Id = id,
                PossibleResults = new string[] { "W1", "W2" },
                Margin = 3
            };
            var betList = new List<Bet>
            {
            new Bet
            {
               Account_Id = id,
               Event_Id = id,
               Choice = "W1",
               Amount = 1000
            },
            new Bet
            {
               Account_Id = defaultWalletId,
               Event_Id = id,
               Choice = "W2",
               Amount = 1000
            }
            };

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);
            _eventRepository.Setup(x => x.UpdateAsync(@event)).Returns(Task.CompletedTask);

            _betRepository.Setup(x => x.GetBetsByEventIdAsync(id)).ReturnsAsync(betList);
            foreach (var settledBet in betList)
            {
                _betRepository.Setup(x => x.UpdateAsync(settledBet)).Returns(Task.CompletedTask);
                _walletRepository.Setup(x => x.GetByIdAsync(settledBet.Account_Id))
                    .ReturnsAsync(wallets.FirstOrDefault(wallet => wallet.Account_Id == settledBet.Account_Id));
                _walletRepository.Setup(x => x.UpdateAsync(
                    wallets.FirstOrDefault(wallet => wallet.Account_Id == settledBet.Account_Id))).Returns(Task.CompletedTask);
            }
           

            _eventService = new EventService(_eventRepository.Object, _participantRepository.Object,
                _sportRepository.Object, _betRepository.Object, _walletRepository.Object, _paramRepository.Object);

            var finishedEvent = await _eventService.FinishEventAsync(eventToFinish);

            var wallet = wallets.FirstOrDefault(wallet => wallet.Amount != 0);

            var bet = betList.FirstOrDefault(x => x.Status != "Bet lost");

            Assert.Equal(1970m, wallet.Amount);
            Assert.Equal($"Win: 1970", bet.Status);
        }

        [Fact]
        public async Task GetEventPreviewAsync_CheckIfItsNoEventWithThisGuidInRepository_NullWasReturned()
        {
            var id = Guid.NewGuid();

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Event)null);

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var eventPreview = await _eventService.GetEventPreviewAsync(id);

            Assert.Null(eventPreview);
        }

        [Fact]
        public async Task GetEventPreviewAsync_CheckIfMethodReturnsEventPreview_EventPreviewWasReturned()
        {
            var id = Guid.NewGuid();

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(
                new Event
                {
                    Id = id,
                    Participant_Id1 = id,
                    Participant_Id2 =id,
                    Sport_Id =1,
                    IsEnded =false,
                    Margin = 5
                }
            );

            _participantRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());
            _sportRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Sport());
            _betRepository.Setup(x => x.GetBetsByEventIdAsync(id)).ReturnsAsync(new List<Bet>
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
            _paramRepository.Setup(x => x.GetParametersByParticipantIdAsync(id)).ReturnsAsync(new List<Parameter>
            {
                new Parameter
                {
                    Id = id
                }
            });

            _eventService = new EventService(_eventRepository.Object, _participantRepository.Object,
                _sportRepository.Object, _betRepository.Object, null, _paramRepository.Object);

            var eventPreview = await _eventService.GetEventPreviewAsync(id);

            Assert.NotNull(eventPreview);
            Assert.Equal(5, eventPreview.Margin);
        }

        [Fact]
        public async Task DeleteEventAsync_CheckIfArgumentExceptionWasThrownIfGuidEmpty_ArgumentExceptionWasThrown()
        {
            _eventService = new EventService(null, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _eventService.DeleteEventAsync(Guid.Empty));
        }

        [Fact]
        public async Task DeleteEventAsync_CheckIfItsUnableToGetEventWithThisId_FalseWasReturned()
        {
            var id = Guid.NewGuid();

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Event)null);

            _eventService = new EventService(_eventRepository.Object, null, null, null, null, null);

            var isEventDeleted = await _eventService.DeleteEventAsync(id);

            Assert.False(isEventDeleted);
        }

        [Fact]
        public async Task DeleteEventAsync_CheckIfItsAvailableToDeleteEvent_TrueWasReturned()
        {
            var id = Guid.NewGuid();

            var @event = new Event 
            {
                Id = id
            };

            var betList = new List<Bet>
            { 
            new Bet
            {
                Event_Id = id,
                Account_Id = id,
                Amount =1000m
            },
            new Bet
            {
               Event_Id = id,
                Account_Id = id,
                Amount =5000m
            }
            };

            var wallet = new Wallet
            {
                Account_Id = id,
                Amount = 0
            };

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);

            _eventRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(@event);


            _betRepository.Setup(x => x.GetBetsByEventIdAsync(id)).ReturnsAsync(betList);
            foreach (var settledBet in betList)
            {
                _betRepository.Setup(x => x.UpdateAsync(settledBet)).Returns(Task.CompletedTask);
                _walletRepository.Setup(x => x.GetByIdAsync(settledBet.Account_Id)).ReturnsAsync(wallet);
                _walletRepository.Setup(x => x.UpdateAsync(wallet)).Returns(Task.CompletedTask);
            }


            _eventService = new EventService(_eventRepository.Object, _participantRepository.Object,
                _sportRepository.Object, _betRepository.Object, _walletRepository.Object, _paramRepository.Object);

            var isEventDeleted = await _eventService.DeleteEventAsync(id);

            Assert.True(isEventDeleted);
            Assert.Equal(6000m, wallet.Amount);
        }
    }
}