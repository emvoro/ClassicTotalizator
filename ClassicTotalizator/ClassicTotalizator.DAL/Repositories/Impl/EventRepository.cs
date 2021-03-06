using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetNotEndedEventsAsync()
        {
            return await Set.Where(x => x.IsEnded == false).ToListAsync();
        }
    }
}