using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;

        public EventService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<EventDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            
            return EventMapper.Map(await _context.Events.FindAsync(id));
        }

        public async Task<EventDTO> CreateEventAsync(EventRegisterDTO eventDTO)
        {
            if (eventDTO == null)
                throw new ArgumentNullException(nameof(eventDTO));
            if (string.IsNullOrEmpty(eventDTO.Participant_Id1.ToString()) ||
                string.IsNullOrEmpty(eventDTO.Participant_Id2.ToString()) ||
                eventDTO.StartTime < DateTimeOffset.UtcNow || eventDTO.Margin <= 0)
                return null;

            var participant1 = await _context.Participants.FindAsync(eventDTO.Participant_Id1);
            var participant2 = await _context.Participants.FindAsync(eventDTO.Participant_Id2);
            var sport = await _context.Sports.FindAsync(eventDTO.SportId);

            var newId = Guid.NewGuid();
            var @event = new Event
            {
                Id = newId,
                BetPool = new BetPool
                {
                    Event_Id = newId,
                    TotalAmount = 0
                },
                IsEnded = false,
                Margin = eventDTO.Margin,
                Participant1 = participant1,
                Participant2 = participant2,
                PossibleResults = eventDTO.PossibleResults,
                Sport = sport,
                Sport_Id = sport.Id,
                StartTime = eventDTO.StartTime,
                Result = null
            };
            
            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();

            return EventMapper.Map(@event);
        }

        public async Task<EventDTO> EditEventAsync(EventDTO newEvent)
        {
            if (newEvent.PossibleResults.Length == 2 && newEvent.PossibleResults.Contains("X"))
                return null;
            
            var oldEvent = await _context.Events.FindAsync(newEvent.Id);
            
            oldEvent.StartTime = newEvent.StartTime;
            oldEvent.Margin = newEvent.Margin;
            oldEvent.IsEnded = newEvent.IsEnded;
            oldEvent.Result = newEvent.EventResult;
            oldEvent.PossibleResults = newEvent.PossibleResults;
            
            _context.Update(oldEvent);
            await _context.SaveChangesAsync();
            
            return EventMapper.Map(oldEvent);
        }

        public async Task<EventsDTO> GetEventsAsync()
        {
            var events = await _context.Events.ToListAsync() ?? new List<Event>();

            return new EventsDTO
            {
                Events = events.Select(EventMapper.Map).ToList()
            };
        }

        public async Task<SportsDTO> GetCurrentListOfSports()
        {
            var sports = await _context.Sports.ToListAsync() ?? new List<Sport>();
            
            return new SportsDTO
            {
                Sports = sports.Select(SportMapper.Map).ToList()
            };
        }

        public async Task<EventsDTO> GetCurrentLineOfEvents()
        {
            var currentLine = await _context
                   .Events.Where(e => e.IsEnded == false).ToListAsync();
            
            return new EventsDTO
            {
                Events = currentLine.Select(EventMapper.Map).ToList()
            };
        }

        public async Task<bool> ClosedEvent(Guid id)
        {
            throw new NotImplementedException();
           /* var @event = await _context.Events.FindAsync(id);
            var totalAmountWithMargin = @event.BetPool.TotalAmount - @event.BetPool.TotalAmount * @event.Margin;*/
        }

        public Task<IEnumerable<EventDTO>> GetEventsBySportAsync(string sport)
        {
            throw new NotImplementedException();
        }
    }
}
