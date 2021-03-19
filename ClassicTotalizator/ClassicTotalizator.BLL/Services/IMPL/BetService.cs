using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
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

        public Task<IEnumerable<BetDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BetDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BetDto> GetByAccountId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddBet(BetDto bet)
        {
            throw new NotImplementedException();
        }
    }
}