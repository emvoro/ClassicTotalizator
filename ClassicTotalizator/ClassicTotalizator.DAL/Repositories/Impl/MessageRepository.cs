using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetLastMessages()
        {
            return await _set.OrderByDescending(x => x.Time).Take(100).ToListAsync();
        }
    }
}