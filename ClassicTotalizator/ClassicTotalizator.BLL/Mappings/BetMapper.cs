using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class BetMapper
    {
        public static Bet Map(BetNewDTO obj)
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
                    Account_Id = obj.Account_Id,
                    Status = obj.Status,
                    Choice = obj.Choice,
                    BetTime = obj.BetTime,
                    Amount = obj.Amount,
                };
        }
    }
}