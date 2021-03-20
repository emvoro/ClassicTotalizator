using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class BetService : IBetService
    {
        private readonly DatabaseContext _context;

        public BetService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BetDto>> GetEventBets(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            return @event.BetPool.Bets.Select(BetMapper.Map).ToList();
        }

        public async Task<BetDto> GetById(Guid id)
        {
            var bet = await _context.Bets.FindAsync(id);

            return BetMapper.Map(bet);
        }

        public async Task<IEnumerable<BetDto>> GetBetsByAccId(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            return account.BetsHistory.Select(BetMapper.Map).ToList();
        }

        public async Task<bool> AddBet(BetDto betDto)
        {
            if (betDto == null)
                throw new ArgumentNullException(nameof(betDto));

            var bet = BetMapper.Map(betDto);
            bet.Id = Guid.NewGuid();
            
            await _context.Bets.AddAsync(bet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}