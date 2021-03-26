using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Wallet
    {
        [Key]
        public Guid Account_Id { get; set; }

        [ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        public decimal Amount { get; set; }
        
        public ICollection<Transaction> TransactionsHistory { get; set; }

        public Wallet()
        {
            TransactionsHistory = new List<Transaction>();
        }
    }
}
