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

        public async Task<IEnumerable<Player>> GetPlayersByParticipantIdAsync(Guid partId)
        {
            return await Set.Where(x => x.Participant_Id == partId).ToListAsync();
        }
    }
}