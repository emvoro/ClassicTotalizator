using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;

namespace ClassicTotalizator.BLL.Services
{
    public interface IWalletService
    {
        Task<WalletDTO> TransactionAsync(Guid accountId, TransactionDTO transactionDto);

        Task<WalletDTO> GetWalletByAccIdAsync(Guid id);

        Task<IEnumerable<TransactionWithTimeDTO>> GetTransactionHistoryByAccIdAsync(Guid id);
    }
}