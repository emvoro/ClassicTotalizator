using System;

namespace ClassicTotalizator.BLL.Contracts.BetDTOs
{
    public class BetNewDTO
    {
        public Guid Event_Id { get; set; }

        public string Choice { get; set; }

        public decimal Amount { get; set; }
    }
}