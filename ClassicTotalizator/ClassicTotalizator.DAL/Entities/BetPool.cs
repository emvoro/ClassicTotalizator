using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class BetPool
    {
        [Key]
        public Guid Event_Id { get; set; }

        public ICollection<Bet> Bets { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal Margin { get; set; }
    }
}
