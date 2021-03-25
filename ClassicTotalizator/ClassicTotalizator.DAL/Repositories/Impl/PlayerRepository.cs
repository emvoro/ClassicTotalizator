using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetPlayersByParticipantId(Guid partId)
        {
            return await _set.Where(x => x.Participant_Id == partId).ToListAsync();
        }
    }
}