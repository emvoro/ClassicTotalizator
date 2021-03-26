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

        public async Task<IEnumerable<Bet>> GetBetsByAccountId(Guid accId)
        {
            return await _set.Where(x => x.Account_Id == accId).ToListAsync();
        }

        public async Task<IEnumerable<Bet>> GetBetsByEventId(Guid eventId)
        {
            return await _set.Where(x => x.Event_Id == eventId).ToListAsync();
        }
    }
}