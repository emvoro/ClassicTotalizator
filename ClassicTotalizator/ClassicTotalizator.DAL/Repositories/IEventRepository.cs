using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.DAL.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsBySportId(int sportId);
    }
}