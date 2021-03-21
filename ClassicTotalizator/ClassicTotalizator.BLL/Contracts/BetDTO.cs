using System;

namespace ClassicTotalizator.BLL.Contracts
{
    public class BetDTO
    {
        public Guid Account_Id { get; set; }
        
        public Guid Event_Id { get; set; }

        public string Choice { get; set; }

        public decimal Amount { get; set; }
    }
}