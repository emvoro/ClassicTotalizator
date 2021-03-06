using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        public BetRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bet>> GetBetsByAccountIdAsync(Guid accId)
        {
            return await Set.Where(x => x.Account_Id == accId).ToListAsync();
        }

        public async Task<IEnumerable<Bet>> GetBetsByEventIdAsync(Guid eventId)
        {
            return await Set.Where(x => x.Event_Id == eventId).ToListAsync();
        }
    }
}