using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IBetRepository : IRepository<Bet>
    {
        Task<IEnumerable<Bet>> GetBetsByAccountIdAsync(Guid accId);
        
        Task<IEnumerable<Bet>> GetBetsByEventIdAsync(Guid eventId);
    }
}