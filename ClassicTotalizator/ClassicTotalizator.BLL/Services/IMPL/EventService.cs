using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Contracts.SportDTOs;

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
            if (participant1 == null)
                return null;

            var participant2 = await _context.Participants.FindAsync(eventDTO.Participant_Id2);
            if (participant2 == null)
                return null;

            var sport = await _context.Sports.FindAsync(eventDTO.SportId);
            if (sport == null)
                return null;

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
                Participant_Id1 = participant1.Id,
                Participant1 = participant1,
                Participant_Id2 = participant2.Id,
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

        public async Task<EventDTO> EditEventAsync(EdittedEventDTO newEvent)
        {
            if (newEvent.StartTime < DateTimeOffset.UtcNow)
                return null;

            var oldEvent = await _context.Events.FindAsync(newEvent.Id);

            oldEvent.Margin = newEvent.Margin;
            oldEvent.StartTime = newEvent.StartTime;

            _context.Update(oldEvent);
            await _context.SaveChangesAsync();

            var editedEvent = EventMapper.Map(oldEvent);
       
            return editedEvent;
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

        public async Task<EventsFeedDTO> GetCurrentLineOfEvents()
        {
            var currentLine = await _context
                   .Events.Where(e => e.IsEnded == false).ToListAsync();

            var eventPreviewDtos = new List<EventPreviewDTO>();

            foreach (var @event in currentLine)
            {
                @event.Participant1 = await _context.Participants.FindAsync(@event.Participant_Id1);
                @event.Participant2 = await _context.Participants.FindAsync(@event.Participant_Id2);
                
                @event.Sport = await _context.Sports.FindAsync(@event.Sport_Id);

                eventPreviewDtos.Add(await GetAmountsOnResults(@event));
            }

            return new EventsFeedDTO
            {
                Events = eventPreviewDtos
            };
        }

        private async Task<EventPreviewDTO> GetAmountsOnResults(Event @event)
        {
            var bets = await _context.Bets.Where(x => x.Event_Id == @event.Id).ToListAsync();
            var eventRes = EventMapper.MapPreview(@event);

            foreach (var bet in bets)
            {
                switch (bet.Choice)
                {
                    case "W1":
                        @eventRes.AmountW1 += bet.Amount;
                        break;
                    case "W2":
                        @eventRes.AmountW2 += bet.Amount;
                        break;
                    case "X":
                        @eventRes.AmountX += bet.Amount;
                        break;
                    default:
                        continue;
                }
            }

            return eventRes;
        }

        public Task<IEnumerable<EventDTO>> GetEventsBySportAsync(string sport)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FinishEvent(FinishedEventDTO eventToClose)
        {
            if (eventToClose == null)
                throw new ArgumentNullException(nameof(eventToClose));

            var closingEvent = await _context.Events.FindAsync(eventToClose.Id);
            if (closingEvent == null)
                return false;
            
            if (!closingEvent.PossibleResults.Contains(eventToClose.Result))
                return false;

            if (closingEvent.IsEnded)
                return false;
           
            closingEvent.Result = eventToClose.Result;
            closingEvent.IsEnded = true;

            _context.Update(closingEvent);
            await _context.SaveChangesAsync();


            return await CashSettlementOfBetsOnEvents(closingEvent);
        }

        private async Task<bool> CashSettlementOfBetsOnEvents(Event closedEvent)
        {
             decimal winningAmount = 0;
             decimal losingAmount = 0;


            var betsInPool = await _context.Bets.Where(id => id.Event_Id == closedEvent.Id).ToListAsync();

             var winningBets = new List<Bet>();
 
             foreach (var bet in betsInPool)
             {
                 if (bet.Choice.Equals(closedEvent.Result))
                 {
                     winningAmount = bet.Amount;
                     winningBets.Add(bet);
                 }
                 else
                     losingAmount = bet.Amount;
             }

             if (winningAmount == 0)
                 return false;

             foreach (var bet in winningBets)
             {
                 var moneyForDep = (losingAmount * bet.Amount) / winningAmount;
                 var pendingWallet = await _context.Wallets.FindAsync(bet.Account_Id);
 
                 pendingWallet.Amount += moneyForDep;
 
                 _context.Wallets.Update(pendingWallet);
                 await _context.SaveChangesAsync();
             }

             return true;
        }
    }
}
