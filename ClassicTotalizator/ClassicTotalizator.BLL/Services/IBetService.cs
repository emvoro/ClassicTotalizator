using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Services
{
    public interface IBetService
    {
        Task<IEnumerable<BetDto>> GetEventBets(Guid id);

        Task<BetDto> GetById(Guid id);

        Task<IEnumerable<BetDto>> GetBetsByAccId(Guid id);

        Task<bool> AddBet(BetDto betDto);
    }
}