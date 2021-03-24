using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IBetService
    {
        Task<IEnumerable<BetDTO>> GetEventBets(Guid id);

        Task<IEnumerable<BetDTO>> GetBetsByAccId(Guid id);

        Task<bool> AddBet(BetNewDTO betDto, Guid accountId);
    }
}