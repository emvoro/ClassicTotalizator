using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Services
{
    public interface IWalletService
    {
        Task<WalletDTO> Transaction(TransactionDTO transactionDto);

        Task<WalletDTO> GetWalletByAccId(Guid id);

        Task<IEnumerable<TransactionDTO>> GetTransactionHistoryByAccId(Guid id);
    }
}