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

        public static BetPreviewForUserDTO MapPreview(Bet obj)
        {
            return obj == null
                ? null
                : new BetPreviewForUserDTO
                {
                    Bet_Id = obj.Id,
                    Status = obj.Status,
                    Choice = obj.Choice,
                    BetTime = obj.BetTime,
                    Amount = obj.Amount,
                };
        }

        public static BetPreviewForAdminsDTO MapPreviewForAdmins(Bet obj)
        {
            return obj == null
                ? null
                : new BetPreviewForAdminsDTO
                {
                    Bet_Id = obj.Id,
                    Account_Id =obj.Account_Id,
                    Status = obj.Status,
                    Choice = obj.Choice,
                    BetTime = obj.BetTime,
                    Amount = obj.Amount,
                };
        }
    }
}