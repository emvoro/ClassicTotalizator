using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Services
{
    public interface IBetService
    {
        Task<IEnumerable<BetDto>> GetAll();

        Task<BetDto> GetById(Guid id);

        Task<BetDto> GetByAccountId(Guid id);

        Task<bool> AddBet(BetDto bet);
    }
}