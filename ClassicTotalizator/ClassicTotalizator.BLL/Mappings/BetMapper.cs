using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class BetMapper
    {
        public static BetDTO Map(Bet obj)
        {
            return obj == null
                ? null
                : new BetDTO
                {
                    Id = obj.Id,
                    Amount = obj.Amount,
                    Account_Id = obj.Account_Id,
                    Choice = obj.Choice,
                    Event_Id = obj.Event_Id
                };
        }
        
        public static Bet Map(BetDTO obj)
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