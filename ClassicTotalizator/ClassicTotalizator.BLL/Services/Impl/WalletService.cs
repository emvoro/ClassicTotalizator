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

        public async Task<WalletDTO> Transaction(Guid accountId, TransactionDTO transactionDto)
        {
            if (transactionDto == null)
                throw new ArgumentNullException(nameof(transactionDto));
            if (!ValidateTransactionDto(transactionDto))
                return null;
            
            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Account_Id == accountId);
            if (wallet == null)
                return null;

            var transaction = TransactionMapper.Map(transactionDto);
            
            transaction.DateTime = DateTimeOffset.UtcNow;
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
            
            transaction.Id = Guid.NewGuid();

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

        public async Task<IEnumerable<TransactionWithTimeDTO>> GetTransactionHistoryByAccId(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var transactions = await _context.Transactions.Where(x => x.Account_Id == id).ToListAsync();

            return transactions.Select(TransactionMapper.MapWithTime).ToList();
        }

        private bool ValidateTransactionDto(TransactionDTO obj)
        {
            if (obj.Amount <= 0 || string.IsNullOrEmpty(obj.Type))
                return false;

            return true;
        }
    }
}