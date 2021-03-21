using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class BetPool
    {
        [Key]
        public Guid Event_Id { get; set; }

        [ForeignKey("Event_Id")]
        public virtual Event Event { get; set; }
        
        public decimal TotalAmount { get; set; }

        public ICollection<Bet> Bets { get; set; }

        public BetPool()
        {
            Bets = new List<Bet>();
        }
    }
}
