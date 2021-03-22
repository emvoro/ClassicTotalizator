using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class TransactionMapper
    {
        public static TransactionDTO Map(Transaction obj)
        {
            return obj == null
                ? null
                : new TransactionDTO
                {
                    Amount = obj.Amount,
                    Type = obj.Type
                };
        } 
        
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
        
        public static Transaction Map(TransactionWithTimeDTO obj)
        {
            return obj == null
                ? null
                : new Transaction
                {
                    DateTime = obj.DateTime,
                    Amount = obj.Amount,
                    Type = obj.Type
                };
        }
        
        public static TransactionWithTimeDTO MapWithTime(Transaction obj)
        {
            return obj == null
                ? null
                : new TransactionWithTimeDTO
                {
                    DateTime = obj.DateTime,
                    Amount = obj.Amount,
                    Type = obj.Type
                };
        }
    }
}