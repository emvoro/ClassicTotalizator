using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<IEnumerable<Player>> GetPlayersByParticipantIdAsync(Guid partId);
    }
}