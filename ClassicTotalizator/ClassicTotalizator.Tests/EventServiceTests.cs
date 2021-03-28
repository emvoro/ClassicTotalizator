using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Services.Impl;
using Xunit;
using ClassicTotalizator.BLL.Mappings;

namespace ClassicTotalizator.Tests
{
    public class EventServiceTests
    {

        private IEventService _eventService;

        private readonly Mock<ISportRepository> _sportRepository = new Mock<ISportRepository>();

        private readonly Mock<IRepository<Wallet>> _walletRepository = new Mock<IRepository<Wallet>>();

        private readonly Mock<IRepository<BetPool>> _betpoolRepository = new Mock<IRepository<BetPool>>();

        private readonly Mock<IRepository<Participant>> _participantRepository = new Mock<IRepository<Participant>>();

        private readonly Mock<IEventRepository> _eventRepository = new Mock<IEventRepository>();

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
        public async void CreateEventAsync_CheckIfNullArgumentExceptionThrown_ExceptionWasThrown()
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
        public async void CreateEventAsync_CheckIfEventWasCreatedNadAddedToDb_EventDtoWasReturned()
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
        public async void EditEventAsync_CheckIfNullWasReturned_NullWasReturned()
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
        public async void EditEventAsync_EventWasCreatedNadAddedToDb_EventDtoWasReturned()
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
    }
}