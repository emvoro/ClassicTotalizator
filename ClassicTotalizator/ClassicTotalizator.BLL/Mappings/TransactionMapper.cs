using ClassicTotalizator.BLL.Contracts;
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
                    Account_Id = obj.Account_Id,
                    DateTime = obj.DateTime,
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
                    Account_Id = obj.Account_Id,
                    DateTime = obj.DateTime,
                    Amount = obj.Amount,
                    Type = obj.Type
                };
        } 
    }
}