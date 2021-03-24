using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class BetMapper
    {
        public static BetDTO Map(Bet bet)
        {
            return bet == null
                ? null
                : new BetDTO
                {
                    Account_Id = bet.Account_Id,
                    Amount = bet.Amount,
                    Choice = bet.Choice,
                    Event_Id = bet.Event_Id
                };
        }
        
        public static Bet Map(BetDTO betDTO)
        {
            return betDTO == null
                ? null
                : new Bet
                {
                    Account_Id = betDTO.Account_Id,
                    Amount = betDTO.Amount,
                    Choice = betDTO.Choice,
                    Event_Id = betDTO.Event_Id
                };
        }

        public static Bet Map(BetNewDTO betNewDTO)
        {
            return betNewDTO == null
                ? null
                : new Bet
                {
                    Amount = betNewDTO.Amount,
                    Choice = betNewDTO.Choice,
                    Event_Id = betNewDTO.Event_Id
                };
        }
    }
}