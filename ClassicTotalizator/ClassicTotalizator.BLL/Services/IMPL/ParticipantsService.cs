using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ParticipantDTO>> GetAllParticipantsAsync()
        {
            var partcipantsListInDal = await _context.Participants.ToListAsync();
            return partcipantsListInDal.Select(ParticipantsMapper.Map).ToList();
        }
    }
}
