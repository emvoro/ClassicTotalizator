using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Wallet
    {
        [Key]
        [Required]
        public Guid User_Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("User_Id")]
        public virtual Account Account { get; set; }

        public ICollection<Transaction> TransactionsHistory { get; set; }
    }
}
