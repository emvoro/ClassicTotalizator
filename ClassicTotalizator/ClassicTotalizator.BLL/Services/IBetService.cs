using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IBetService
    {
        Task<IEnumerable<BetPreviewForAdminsDTO>> GetAllEventBets();

        Task<IEnumerable<BetPreviewForUserDTO>> GetBetsByAccId(Guid id);

        Task<bool> AddBet(BetNewDTO betDto, Guid accountId);


    }
}