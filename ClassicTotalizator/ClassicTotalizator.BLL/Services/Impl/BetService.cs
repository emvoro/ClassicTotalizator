using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.Impl
{
    public class BetService : IBetService
    {

        private readonly IBetRepository _repository;

        private readonly IRepository<Wallet> _walletRepository;

        private readonly IRepository<BetPool> _betpoolRepository;

        private readonly IRepository<Participant> _participantRepository;

        private readonly IRepository<Event> _eventRepository;

        public BetService(IBetRepository repository,
            IRepository<Wallet> walletRepository,
            IRepository<BetPool> betpoolRepository,
            IRepository<Event> eventRepository,
            IRepository<Participant> participantRepository)
        {
            _repository = repository;
            _walletRepository = walletRepository;
            _betpoolRepository = betpoolRepository;
            _eventRepository = eventRepository;
            _participantRepository = participantRepository;
        }

        public async Task<IEnumerable<BetPreviewForAdminsDTO>> GetAllEventBetsAsync()
        {
            var bets = await _repository.GetAllAsync();

            var previewBets = new List<BetPreviewForAdminsDTO>();

            foreach (var bet in bets)
            {
                var currentEvent = await _eventRepository.GetByIdAsync(bet.Event_Id);
                var previewBet = BetMapper.MapPreviewForAdmins(bet);
                previewBet.EventStartime = currentEvent.StartTime;

                previewBet.TeamConfrontation = await GetTeamConfrontation(currentEvent.Participant_Id1, currentEvent.Participant_Id2);
                previewBets.Add(previewBet);
            }

            return previewBets;
        }

        public async Task<IEnumerable<BetPreviewForUserDTO>> GetBetsByAccIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var bets = await _repository.GetBetsByAccountIdAsync(id);

            var previewBets = new List<BetPreviewForUserDTO>();

            foreach (var bet in bets)
            {
                var currentEvent = await _eventRepository.GetByIdAsync(bet.Event_Id);
                var previewBet = BetMapper.MapPreview(bet);
                previewBet.EventStartime = currentEvent.StartTime;

                previewBet.TeamConfrontation = await GetTeamConfrontation(currentEvent.Participant_Id1, currentEvent.Participant_Id2);
                previewBets.Add(previewBet);
            }

            return previewBets;
        }

        public async Task<bool> AddBetAsync(BetNewDTO betDto, Guid accountId)
        {
            if (betDto == null)
                throw new ArgumentNullException(nameof(betDto));
            if (betDto.Amount <= 0 || betDto.Event_Id == Guid.Empty || string.IsNullOrEmpty(betDto.Choice) || accountId == Guid.Empty)
                return false;

            var @event = await _eventRepository.GetByIdAsync(betDto.Event_Id);
            if (@event == null)
                return false;

            if (@event.StartTime < DateTimeOffset.UtcNow || @event.IsEnded)
                return false;

            var betPool = await _betpoolRepository.GetByIdAsync(@event.Id);
            if (betPool == null)
                return false;

            var wallet = await _walletRepository.GetByIdAsync(accountId);
            if (wallet == null || wallet.Amount < betDto.Amount)
                return false;

            wallet.Amount -= betDto.Amount;
            var bet = BetMapper.Map(betDto);
            bet.Id = Guid.NewGuid();
            bet.Account_Id = accountId;
            bet.Status = "Active";
            bet.BetTime = DateTimeOffset.UtcNow;

            betPool.TotalAmount += bet.Amount;

            await _betpoolRepository.UpdateAsync(betPool);
            await _walletRepository.UpdateAsync(wallet);
            await _repository.AddAsync(bet);

            return true;
        }

        private async Task<string> GetTeamConfrontation(Guid participantId1, Guid participantId2)
        {
            var participantPreview1 = await _participantRepository.GetByIdAsync(participantId1);
            var participantPreview2 = await _participantRepository.GetByIdAsync(participantId2);

            return $"{participantPreview1.Name} - {participantPreview2.Name}";
        }
    }
}