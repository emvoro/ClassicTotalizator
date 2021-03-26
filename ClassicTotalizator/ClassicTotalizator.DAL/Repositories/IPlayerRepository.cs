using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayersByParticipantId(Guid partId);
    }
}