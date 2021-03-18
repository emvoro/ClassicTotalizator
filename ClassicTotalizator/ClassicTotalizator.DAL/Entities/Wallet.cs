using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Wallet
    {
        [Key]
        public Guid Account_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual Account Account { get; set; }

        public decimal Amount { get; set; }

        public ICollection<Transaction> TransactionsHistory { get; set; }

        public Wallet()
        {
            TransactionsHistory = new List<Transaction>();
        }
    }
}
