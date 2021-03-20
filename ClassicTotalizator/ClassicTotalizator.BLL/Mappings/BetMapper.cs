using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class BetMapper
    {
        public static BetDto Map(Bet obj)
        {
            return obj == null
                ? null
                : new BetDto
                {
                    Id = obj.Id,
                    Amount = obj.Amount,
                    Account_Id = obj.Account_Id,
                    Choice = obj.Choice,
                    Event_Id = obj.Event_Id
                };
        }
        
        public static Bet Map(BetDto obj)
        {
            return obj == null
                ? null
                : new Bet
                {
                    Id = obj.Id,
                    Amount = obj.Amount,
                    Account_Id = obj.Account_Id,
                    Choice = obj.Choice,
                    Event_Id = obj.Event_Id
                };
        }
    }
}