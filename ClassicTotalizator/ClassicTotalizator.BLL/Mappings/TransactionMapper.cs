using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class TransactionMapper
    {
        public static Transaction Map(TransactionDTO obj)
        {
            return obj == null
                ? null
                : new Transaction
                {
                    Amount = obj.Amount,
                    Type = obj.Type
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