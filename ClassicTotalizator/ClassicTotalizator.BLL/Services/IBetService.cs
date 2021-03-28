using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.BetDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IBetService
    {
        Task<IEnumerable<BetPreviewForAdminsDTO>> GetAllEventBetsAsync();

        Task<IEnumerable<BetPreviewForUserDTO>> GetBetsByAccIdAsync(Guid id);

        Task<bool> AddBetAsync(BetNewDTO betDto, Guid accountId);
    }
}