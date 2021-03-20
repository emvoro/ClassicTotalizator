using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Services
{
    public interface IWalletService
    {
        Task<WalletDTO> GetWalletByAccId(Guid id);

        Task<IEnumerable<TransactionDTO>> GetTransactionHistoryByAccId(Guid id);
    }
}