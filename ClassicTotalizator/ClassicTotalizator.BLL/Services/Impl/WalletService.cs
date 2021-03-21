using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class WalletService : IWalletService
    {
        private readonly DatabaseContext _context;

        public WalletService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<WalletDTO> Transaction(TransactionDTO transactionDto)
        {
            if (transactionDto == null)
                throw new ArgumentNullException(nameof(transactionDto));
            if (!ValidateTransactionDto(transactionDto))
                return null;

            transactionDto.DateTime = DateTimeOffset.UtcNow;
            
            var transaction = TransactionMapper.Map(transactionDto);
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Account_Id == transaction.Account_Id);
            if (wallet == null)
                return null;

            transaction.Id = Guid.NewGuid();

            transaction.Wallet = wallet;
            
            if(transaction.Type == "withdraw")
            {
                if (wallet.Amount < transaction.Amount)
                    return null;
                
                wallet.Amount -= transaction.Amount;
            }
            else if (transaction.Type == "deposit")
            {
                wallet.Amount += transaction.Amount;
            }
            else
            {
                return null;
            }
            
            wallet.TransactionsHistory.ToList().Add(transaction);

            _context.Wallets.Update(wallet);
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return WalletMapping.Map(wallet);
        }

        public async Task<WalletDTO> GetWalletByAccId(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Account_Id == id);

            return WalletMapping.Map(wallet);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionHistoryByAccId(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Account_Id == id);

            return wallet.TransactionsHistory.Select(TransactionMapper.Map).ToList();
        }

        private bool ValidateTransactionDto(TransactionDTO obj)
        {
            if (obj.Amount <= 0 || string.IsNullOrEmpty(obj.Type) || obj.Account_Id == Guid.Empty)
                return false;

            return true;
        }
    }
}