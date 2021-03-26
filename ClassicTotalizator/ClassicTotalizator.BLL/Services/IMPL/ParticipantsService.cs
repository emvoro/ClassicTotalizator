using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;
using ClassicTotalizator.BLL.Contracts.PlayerDTOs;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IRepository<Participant> _repository;

        private readonly IParameterRepository _parameterRepository;

        private readonly IPlayerRepository _playerRepository;
        
        public ParticipantsService(IRepository<Participant> repository, 
            IParameterRepository parameterRepository, 
            IPlayerRepository playerRepository)
        {
            _repository = repository;
            _parameterRepository = parameterRepository;
            _playerRepository = playerRepository;
        }

        public async Task<ParticipantsDTO> GetAllParticipantsAsync()
        {
            var participantsListInDal = await _repository.GetAllAsync();
            if (participantsListInDal == null) 
                return null;

            var participants = new ParticipantsDTO
            {
                Participants = participantsListInDal.Select(ParticipantsMapper.Map).ToList()
            };
            
            foreach (var participant in participants.Participants)
            {
                participant.Players = await GetPlayersByPartId(participant.Id);
                participant.Parameters = await GetParametersByPartId(participant.Id);
            }
            
            return participants;
        }

        public async Task<ParticipantDTO> AddNewParticipant(ParticipantRegisterDTO participantRegisterDto)
        {
            if (participantRegisterDto == null) 
                return null;
            
            var newGuid = Guid.NewGuid();
            var participant = ParticipantsMapper.Map(participantRegisterDto);
            participant.Id = newGuid;
            
            var mainPlayer = participant.Players.FirstOrDefault();
            if (mainPlayer != null && mainPlayer.Name == participant.Name)
            {
                mainPlayer.Id = participant.Id;
                mainPlayer.Participant_Id = newGuid;
            }
            else
            {
                foreach (var player in participant.Players)
                {
                    player.Id = Guid.NewGuid();
                    player.Participant_Id = newGuid;
                }
            }

            await _repository.AddAsync(participant);

            return ParticipantsMapper.Map(participant);
        }

        public async Task<bool> DeleteParticipantAsync(Guid id)
        {
            var participant = await _repository.GetByIdAsync(id);

            if (participant == null) 
                return false;

            await _repository.RemoveAsync(participant);

            return true;
        }

        private async Task<ICollection<ParameterDTO>> GetParametersByPartId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var parameters = await _parameterRepository.GetParametersByParticipantId(id);

            return parameters.Select(ParameterMapper.Map).ToList();
        }

        private async Task<ICollection<PlayerDTO>> GetPlayersByPartId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var players = await _playerRepository.GetPlayersByParticipantId(id);

            return players.Select(PlayerMapper.Map).ToList();
        }
    }
}
