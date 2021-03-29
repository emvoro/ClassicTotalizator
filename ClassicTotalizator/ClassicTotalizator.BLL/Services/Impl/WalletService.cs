using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.Impl
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

        public async Task<WalletDTO> TransactionAsync(Guid accountId, TransactionDTO transactionDto)
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

            if (transaction.Type == "withdraw")
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
            transaction.Wallet_Id = accountId;

            await _repository.UpdateAsync(wallet);
            await _transactionRepository.AddAsync(transaction);

            return WalletMapping.Map(wallet);
        }

        public async Task<WalletDTO> GetWalletByAccIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var wallet = await _repository.GetByIdAsync(id);

            return WalletMapping.Map(wallet);
        }

        public async Task<IEnumerable<TransactionWithTimeDTO>> GetTransactionHistoryByAccIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var transactions = await _transactionRepository.GetAccountTransactionAsync(id);

            return transactions.Select(TransactionMapper.MapWithTime).ToList();
        }

        private static bool ValidateTransactionDto(TransactionDTO transactionDto)
        {
            if (transactionDto.Amount <= 0 || string.IsNullOrEmpty(transactionDto.Type))
                return false;

            return true;
        }
    }
}