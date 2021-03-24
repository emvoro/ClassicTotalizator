using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly DatabaseContext _context;
        
        public ParticipantsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ParticipantsDTO> GetAllParticipantsAsync()
        {
            var participantsListInDal = await _context.Participants.ToListAsync();

            if (participantsListInDal == null) return null;

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

        public async Task<ParticipantDTO> AddNewParticipant(ParticipantRegisterDTO participantRegisterDTO)
        {
            if (participantRegisterDTO == null) return null;
            
            var newGuid = Guid.NewGuid();
            var participant = ParticipantsMapper.Map(participantRegisterDTO);
            participant.Id = newGuid;
            var mainPlayer = participant.Players.FirstOrDefault();

            if (mainPlayer.Name == participant.Name)
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

            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();

            return ParticipantsMapper.Map(participant);
        }

        public async Task<bool> DeleteParticipantAsync(Guid id)
        {
            var participant = _context.Participants.FirstOrDefault(x => x.Id == id);

            if (participant == null) return false;

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<IEnumerable<ParameterDTO>> GetParametersByPartId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var parameters = await _context.Parameters.Where(x => x.Participant_Id == id).ToListAsync();

            return parameters.Select(ParameterMapper.Map).ToList();
        }

        private async Task<IEnumerable<PlayerDTO>> GetPlayersByPartId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var players = await _context.Players.Where(x => x.Participant_Id == id).ToListAsync();

            return players.Select(PlayerMapper.Map).ToList();
        }
    }
}
