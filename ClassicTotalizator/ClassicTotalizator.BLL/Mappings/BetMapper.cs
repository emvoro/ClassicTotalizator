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
                    Amount = obj.Amount,
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
                    Amount = obj.Amount,
                    Choice = obj.Choice,
                    Event_Id = obj.Event_Id
                };
        }
    }
}