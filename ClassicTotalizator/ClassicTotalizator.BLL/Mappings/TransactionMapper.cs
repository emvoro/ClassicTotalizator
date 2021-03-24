using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class TransactionMapper
    {
        public static TransactionDTO Map(Transaction transaction)
        {
            return transaction == null
                ? null
                : new TransactionDTO
                {
                    Amount = transaction.Amount,
                    Type = transaction.Type
                };
        } 
        
        public static Transaction Map(TransactionDTO transactionDTO)
        {
            return transactionDTO == null
                ? null
                : new Transaction
                {
                    Amount = transactionDTO.Amount,
                    Type = transactionDTO.Type
                };
        } 
        
        public static Transaction Map(TransactionWithTimeDTO transactionWithTimeDTO)
        {
            return transactionWithTimeDTO == null
                ? null
                : new Transaction
                {
                    DateTime = transactionWithTimeDTO.DateTime,
                    Amount = transactionWithTimeDTO.Amount,
                    Type = transactionWithTimeDTO.Type
                };
        }
        
        public static TransactionWithTimeDTO MapWithTime(Transaction transaction)
        {
            return transaction == null
                ? null
                : new TransactionWithTimeDTO
                {
                    DateTime = transaction.DateTime,
                    Amount = transaction.Amount,
                    Type = transaction.Type
                };
        }
    }
}