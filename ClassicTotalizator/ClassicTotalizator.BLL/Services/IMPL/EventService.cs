using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        private readonly IRepository<Participant> _participantRepository;

        private readonly ISportRepository _sportRepository;

        private readonly IBetRepository _betRepository;

        private readonly IRepository<Wallet> _walletRepository;

        private readonly IParameterRepository _parameterRepository;

        public EventService(IEventRepository repository,
            IRepository<Participant> participantRepository,
            ISportRepository sportRepository,
            IBetRepository betRepository,
            IRepository<Wallet> walletRepository, 
            IParameterRepository parameterRepository)
        {
            _repository = repository;
            _participantRepository = participantRepository;
            _sportRepository = sportRepository;
            _betRepository = betRepository;
            _walletRepository = walletRepository;
            _parameterRepository = parameterRepository;
        }

        public async Task<EventDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return EventMapper.Map(await _repository.GetByIdAsync(id));
        }

        public async Task<EventDTO> CreateEventAsync(EventRegisterDTO eventDto)
        {
            if (eventDto == null)
                throw new ArgumentNullException(nameof(eventDto));

            if (string.IsNullOrEmpty(eventDto.Participant_Id1.ToString()) ||
                string.IsNullOrEmpty(eventDto.Participant_Id2.ToString()) ||
                eventDto.StartTime < DateTimeOffset.UtcNow || eventDto.Margin <= 0)
                return null;

            if (eventDto.Margin <= 0 && eventDto.Margin > 100)
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
                Margin = eventDto.Margin,
                Participant_Id1 = eventDto.Participant_Id1,
                Participant_Id2 = eventDto.Participant_Id2,
                PossibleResults = eventDto.PossibleResults,
                Sport_Id = eventDto.SportId,
                StartTime = eventDto.StartTime,
                Result = null
            };

            await _repository.AddAsync(@event);

            return EventMapper.Map(@event);
        }

        public async Task<EventDTO> EditEventAsync(EditedEventDTO editedEventDto)
        {
            if (editedEventDto.StartTime < DateTimeOffset.UtcNow || (editedEventDto.Margin <= 0 && editedEventDto.Margin > 100))
                return null;

            var oldEvent = await _repository.GetByIdAsync(editedEventDto.Id);

            oldEvent.Margin = editedEventDto.Margin;
            oldEvent.StartTime = editedEventDto.StartTime;

            await _repository.UpdateAsync(oldEvent);

            var editedEvent = EventMapper.Map(oldEvent);

            return editedEvent;
        }

        public async Task<EventsFeedDTO> GetEventsAsync()
        {
            var events = await _repository.GetAllAsync() ?? new List<Event>();

            var eventPreviewDtOs = new List<EventPreviewDTO>();

            foreach (var @event in events)
            {
                @event.Participant1 = await _participantRepository.GetByIdAsync(@event.Participant_Id1);
                @event.Participant2 = await _participantRepository.GetByIdAsync(@event.Participant_Id2);
                @event.Sport = await _sportRepository.GetByIdAsync(@event.Sport_Id);
                eventPreviewDtOs.Add(await GetAmountsOnResults(@event));
            }

            return new EventsFeedDTO
            {
                Events = eventPreviewDtOs
            };
        }

        public async Task<EventsFeedDTO> GetCurrentLineOfEvents()
        {
            var currentLine = await _repository.GetNotEndedEvents();

            var eventPreviewDtOs = new List<EventPreviewDTO>();

            foreach (var @event in currentLine)
            {
                @event.Participant1 = await _participantRepository.GetByIdAsync(@event.Participant_Id1);
                @event.Participant2 = await _participantRepository.GetByIdAsync(@event.Participant_Id2);
                @event.Sport = await _sportRepository.GetByIdAsync(@event.Sport_Id);
                eventPreviewDtOs.Add(await GetAmountsOnResults(@event));
            }

            return new EventsFeedDTO
            {
                Events = eventPreviewDtOs
            };
        }

        public async Task<bool> FinishEvent(FinishedEventDTO eventToClose)
        {
            if (eventToClose == null)
                throw new ArgumentNullException(nameof(eventToClose));

            var closingEvent = await _repository.GetByIdAsync(eventToClose.Id);
            if (closingEvent == null)
                return false;

            if (!closingEvent.PossibleResults.Contains(eventToClose.Result))
                return false;

            if (closingEvent.IsEnded)
                return false;

            closingEvent.Result = eventToClose.Result;
            closingEvent.IsEnded = true;

            await _repository.UpdateAsync(closingEvent);

            return await CashSettlementOfBetsOnEvents(closingEvent);
        }

        public async Task<EventPreviewDTO> GetEventPreview(Guid id)
        {
            var eventInBase = await _repository.GetByIdAsync(id);
            if (eventInBase == null)
                return null;

            eventInBase.Participant1 = await _participantRepository.GetByIdAsync(eventInBase.Participant_Id1);
            eventInBase.Participant2 = await _participantRepository.GetByIdAsync(eventInBase.Participant_Id2);
            eventInBase.Sport = await _sportRepository.GetByIdAsync(eventInBase.Sport_Id);

            return await GetAmountsOnResults(eventInBase);
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                throw new ArgumentException();

            var eventToDelete = await _repository.GetByIdAsync(id);
            if (eventToDelete == null)
                return false;

            var betPool = await _betRepository.GetBetsByEventId(id);

            foreach (var bet in betPool)
            {
                var pendingWallet = await _walletRepository.GetByIdAsync(bet.Account_Id);
                pendingWallet.Amount += bet.Amount;
                await _walletRepository.UpdateAsync(pendingWallet);

                bet.Status = $"Refund";
                await _betRepository.UpdateAsync(bet);
            }

            await _repository.RemoveAsync(eventToDelete);

            return true;
        }

        private async Task<EventPreviewDTO> GetAmountsOnResults(Event @event)
        {
            var bets = await _betRepository.GetBetsByEventId(@event.Id);
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

            var parameters1 = await _parameterRepository.GetParametersByParticipantId(eventRes.Participant1.Id);
            var parameters2 = await _parameterRepository.GetParametersByParticipantId(eventRes.Participant2.Id);
            foreach (var parameter in parameters1)
            {
                eventRes.Participant1.Parameters.Add(ParameterMapper.Map(parameter));
            }

            foreach (var parameter in parameters2)
            {
                eventRes.Participant2.Parameters.Add(ParameterMapper.Map(parameter));
            }

            return eventRes;
        }

        private async Task<bool> CashSettlementOfBetsOnEvents(Event closedEvent)
        {
            decimal winningAmount = 0;
            decimal losingAmount = 0;

            var betsInPool = await _betRepository.GetBetsByEventId(closedEvent.Id);

            var winningBets = new List<Bet>();

            var losingBets = new List<Bet>();

            foreach (var bet in betsInPool)
            {
                if (bet.Choice.Equals(closedEvent.Result))
                {
                    winningAmount = bet.Amount;
                    winningBets.Add(bet);
                }
                else
                {
                    losingAmount = bet.Amount;
                    losingBets.Add(bet);
                }
            }

            foreach (var bet in losingBets)
            {
                bet.Status = $"Bet lost";
                await _betRepository.UpdateAsync(bet);
            }

            if (winningAmount == 0) return true;


            foreach (var bet in winningBets)
            {
                var pendingWallet = await _walletRepository.GetByIdAsync(bet.Account_Id);
                if (losingAmount == 0)
                {
                    var refundedMoney = bet.Amount - (closedEvent.Margin / 100m);
                    pendingWallet.Amount += refundedMoney;
                   
                    await _walletRepository.UpdateAsync(pendingWallet);

                    bet.Status = $"Win(No losing bets): {refundedMoney}";
                    await _betRepository.UpdateAsync(bet);

                    continue;
                }

                var betPart = bet.Amount / winningAmount * (100m - closedEvent.Margin);
                var moneyForDep = losingAmount / 100m * betPart;

                pendingWallet.Amount += bet.Amount + moneyForDep;

                await _walletRepository.UpdateAsync(pendingWallet);

                bet.Status = $"Win: {moneyForDep}";
                await _betRepository.UpdateAsync(bet);                
            }

            return true;
        }
    }
}
