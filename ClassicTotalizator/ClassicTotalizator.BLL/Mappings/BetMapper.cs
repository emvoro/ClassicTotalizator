using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
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
                    Account_Id = obj.Account_Id,
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
                    Account_Id = obj.Account_Id,
                    Amount = obj.Amount,
                    Choice = obj.Choice,
                    Event_Id = obj.Event_Id
                };
        }
    }
}