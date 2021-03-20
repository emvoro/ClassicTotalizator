using System;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Contracts
{
    public class BetDto
    {
        public Guid Id { get; set; }

        public Guid Account_Id { get; set; }

        public Guid Event_Id { get; set; }
        
        public virtual BetPool BetPool { get; set; }

        public string Choice { get; set; }

        public decimal Amount { get; set; }
    }
}