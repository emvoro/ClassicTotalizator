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
    }
}