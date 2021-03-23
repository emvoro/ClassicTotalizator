using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;
using ClassicTotalizator.DAL.Entities;

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

        public async Task<ParticipantDTO> AddNewParticipant(ParticipantRegisterDTO participant)
        {
            if (participant == null)
                return null;
            
            var newGuid = Guid.NewGuid();
            var newParticipant = ParticipantsMapper.Map(participant);
            newParticipant.Id = newGuid;

            var mainPlayer = newParticipant.Players.FirstOrDefault();
            if (mainPlayer.Name == newParticipant.Name)
            {
                mainPlayer.Id = newParticipant.Id;

                mainPlayer.Participant_Id = newGuid;
            }
            else
            {
                foreach (var p in newParticipant.Players)
                {
                    p.Id = Guid.NewGuid();
                    p.Participant_Id = newGuid;
                }
            }
            await _context.Participants.AddAsync(newParticipant);
            await _context.SaveChangesAsync();

            return ParticipantsMapper.Map(newParticipant);
        }

        private async Task<IEnumerable<ParameterDTO>> GetParametersByPartId(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var parameters = await _context.Parameters.Where(x => x.Participant_Id == id).ToListAsync();
            return parameters.Select(ParameterMapper.Map).ToList();
        }

        private async Task<IEnumerable<PlayerDTO>> GetPlayersByPartId(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var players = await _context.Players.Where(x => x.Participant_Id == id).ToListAsync();
            return players.Select(PlayerMapper.Map).ToList();
        }
    }
}
