using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassicTotalizator.DAL.Repositories.Impl
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransaction(Guid accountId)
        {
            return await Set.Where(x => x.Wallet_Id == accountId).ToListAsync();
        }
    }
}