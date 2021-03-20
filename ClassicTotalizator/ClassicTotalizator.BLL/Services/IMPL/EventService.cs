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
                return null;

            var newEvent = EventMapper.Map(eventDTO);
            newEvent.Id = Guid.NewGuid();
            newEvent.BetPool = new BetPool { Event_Id = newEvent.Id, TotalAmount = 0 };
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();

            return EventMapper.Map(newEvent);
        }

        public async Task<Event> EditEventAsync(EventDTO newEvent)
        {
            var oldEvent = await _context.Events.FindAsync(newEvent.Id);
            oldEvent.Participant1 = newEvent.Participant1;
            oldEvent.Participant2 = newEvent.Participant2;
            oldEvent.Sport = SportMapper.Map(newEvent.Sport);
            oldEvent.StartTime = newEvent.StartTime;
            oldEvent.Margin = newEvent.Margin;
            oldEvent.IsEnded = newEvent.IsEnded;
            oldEvent.Result = newEvent.EventResult;

            if (newEvent.PossibleResults.Length == 2 && newEvent.PossibleResults.Contains("X"))
                return null;
            oldEvent.PossibleResults = newEvent.PossibleResults;
            _context.Update(oldEvent);
            await _context.SaveChangesAsync();

            return oldEvent;
        }

        public async Task<IEnumerable<EventDTO>> GetEventsAsync()
        {
            var events = await _context.Events.Where(x => x.StartTime < DateTimeOffset.UtcNow).ToListAsync() ?? new List<Event>();

            return events.Select(EventMapper.Map).ToList();
        }

        public Task<IEnumerable<EventDTO>> GetEventsBySportAsync(string sport)
        {
            throw new NotImplementedException();
        }
    }
}
