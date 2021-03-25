using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IBetRepository
    {
        Task<IEnumerable<Bet>> GetBetsByAccountId(Guid accId);
        
        Task<IEnumerable<Bet>> GetBetsByEventId(Guid eventId);
    }
}