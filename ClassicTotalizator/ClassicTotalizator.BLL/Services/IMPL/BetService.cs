using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class BetService : IBetService
    {
        private readonly DatabaseContext _context;

        private readonly IEventService _eventService;

        public BetService(DatabaseContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        public async Task<IEnumerable<BetDTO>> GetEventBets(Guid id)
        {
            var bets = await _context.Bets.Where(x => x.Event_Id == id).ToListAsync();

            return bets.Select(BetMapper.Map).ToList();
        }

        public async Task<IEnumerable<BetDTO>> GetBetsByAccId(Guid id)
        {
            var bets = await _context.Bets.Where(x => x.Account_Id == id).ToListAsync();

            return bets.Select(BetMapper.Map).ToList();
        }

        public async Task<bool> AddBet(BetDTO betDto)
        {
            if (betDto == null)
                throw new ArgumentNullException(nameof(betDto));
            if (betDto.Amount <= 0 || betDto.Event_Id == Guid.Empty ||  string.IsNullOrEmpty(betDto.Choice))
                return false;
            
            var @event = await _eventService.GetById(betDto.Event_Id);
            if (@event == null)
                return false;

            if (@event.StartTime < DateTimeOffset.UtcNow || @event.IsEnded)
                return false;

            var betPool = await _context.BetPools.FirstOrDefaultAsync(x => x.Event_Id == @event.Id);
            if (betPool == null)
                return false;
            
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Account_Id == betDto.Account_Id);
            if (wallet == null || wallet.Amount < betDto.Amount)
                return false;
            
            wallet.Amount -= betDto.Amount;

            var bet = BetMapper.Map(betDto);
            bet.Id = Guid.NewGuid();
            bet.Account = await _context.Accounts.FindAsync(bet.Account_Id);
            
            betPool.Bets.Add(bet);
            betPool.TotalAmount += bet.Amount;

            _context.BetPools.Update(betPool);
            _context.Wallets.Update(wallet);
            await _context.Bets.AddAsync(bet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}