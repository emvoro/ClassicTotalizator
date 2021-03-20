using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Event> GetById(Guid id)
        {
            if (id == Guid.Empty) return null;

            return await _context.Events.FindAsync(id);
        }

        public async Task<bool> CreateEventAsync(EventDTO eventDTO)
        {
            if (eventDTO == null)
                return false;

            var newEvent = EventMapper.Map(eventDTO);
            newEvent.Id = Guid.NewGuid();
            newEvent.BetPool = new BetPool { Event_Id = newEvent.Id, Margin = eventDTO.Margin, TotalAmount = 0 };
            newEvent.EventImage = "";

            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();

            return true;
        }

        // change entity : EndTime => isEnded, EventResult => Result(int)
        public async Task<Event> EndEventAsync(Guid id, bool isEnded, string result)
        {
            var newEvent = _context.Events.FindAsync(id).Result;

            newEvent.EventResult = result;
            newEvent.EndTime = DateTimeOffset.Now;
            // ??????
            _context.Update(newEvent);
            await _context.SaveChangesAsync();

            return newEvent;
        }

        public async Task<Event> EditEventAsync(Guid id, EventDTO newEvent)
        {
            var oldEvent = _context.Events.FindAsync(id).Result;
            oldEvent.Participant1 = newEvent.Participant1;
            oldEvent.Participant2 = newEvent.Participant2;
            oldEvent.Sport = newEvent.Sport;
            oldEvent.StartTime = newEvent.StartTime;
            oldEvent.BetPool.Margin = newEvent.Margin;

            _context.Update(oldEvent);
            await _context.SaveChangesAsync();

            return oldEvent;
        }

        public async Task<IEnumerable<EventDTO>> GetEventsAsync()
        {
            var events = await _context.Events
                .Where(x => x.StartTime < DateTimeOffset.Now)
                .ToListAsync() ?? new List<Event>();

            return events.Select(EventMapper.Map).ToList();
        }
    }
}
