using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var partcipantsListInDal = await _context.Participants.ToListAsync();
            return new ParticipantsDTO() { Participants = partcipantsListInDal.Select(ParticipantsMapper.Map).ToList() };
        }

        public async Task<bool> AddNewParticipant(ParticipantDTO participant)
        {
            if (participant == null)
                return false;

            var newParticipant = ParticipantsMapper.Map(participant);

            await _context.Participants.AddAsync(newParticipant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
