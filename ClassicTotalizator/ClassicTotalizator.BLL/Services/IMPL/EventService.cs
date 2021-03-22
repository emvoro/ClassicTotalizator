﻿using ClassicTotalizator.BLL.Contracts;
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

            var editedEvent = EventMapper.Map(oldEvent);

            if (editedEvent.IsEnded)
                await CashSettlementOfBetsOnEvents(editedEvent);



            return editedEvent;
        }

        //ToDo: Add margin distinction
        private async Task CashSettlementOfBetsOnEvents(EventDTO closedEvent)
        {
            var @event = await _context.Events.FindAsync(closedEvent.Id);

            decimal winningAmount = 0;
            decimal losingAmount = 0;

            var currentBetPool = await _context.BetPools.FindAsync(closedEvent.Id);

            var winningBets = new List<Bet>();

            foreach (var m in currentBetPool.Bets)
            {
                if (m.Choice.Equals(closedEvent.EventResult))
                {
                    winningAmount = m.Amount;
                    winningBets.Add(m);
                }
                else
                    losingAmount = m.Amount;
            }

            var totalAMount = winningAmount + losingAmount;
            
            if (winningAmount == 0)
                return;
            

            foreach (var m in winningBets)
            {
                var moneyForDep = (losingAmount * m.Amount) / winningAmount;
                var pendingWallet = await _context.Wallets.FindAsync(m.Account_Id);

                pendingWallet.Amount += moneyForDep;

                _context.Wallets.Update(pendingWallet);
                await _context.SaveChangesAsync();
            }
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

        public Task<IEnumerable<EventDTO>> GetEventsBySportAsync(string sport)
        {
            throw new NotImplementedException();
        }
    }
}
