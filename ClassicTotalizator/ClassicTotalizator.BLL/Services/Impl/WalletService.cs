using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _repository;

        private readonly ITransactionRepository _transactionRepository;

        public WalletService(IRepository<Wallet> repository, 
            ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
        }

        public async Task<WalletDTO> Transaction(Guid accountId, TransactionDTO transactionDto)
        {
            if (transactionDto == null) 
                throw new ArgumentNullException(nameof(transactionDto));
            if (!ValidateTransactionDto(transactionDto)) 
                return null;
            
            var wallet = await _repository.GetByIdAsync(accountId);
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
                wallet.Amount += transaction.Amount;
            else 
                return null;
            
            transaction.Id = Guid.NewGuid();

            await _repository.UpdateAsync(wallet);
            await _transactionRepository.AddAsync(transaction);

            return WalletMapping.Map(wallet);
        }

        public async Task<WalletDTO> GetWalletByAccId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var wallet = await _repository.GetByIdAsync(id);

            return WalletMapping.Map(wallet);
        }

        public async Task<IEnumerable<TransactionWithTimeDTO>> GetTransactionHistoryByAccId(Guid id)
        {
            if (id == Guid.Empty) return null;

            var transactions = await _transactionRepository.GetAccountTransaction(id);

            return transactions.Select(TransactionMapper.MapWithTime).ToList();
        }

        private static bool ValidateTransactionDto(TransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount <= 0 || string.IsNullOrEmpty(transactionDTO.Type)) 
                return false;

            return true;
        }
    }
}