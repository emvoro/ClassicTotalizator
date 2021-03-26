using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid Wallet_Id { get; set; }

        [ForeignKey("Wallet_Id")]
        public virtual Wallet Wallet { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
