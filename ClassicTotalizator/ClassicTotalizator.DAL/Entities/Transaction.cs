using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.DAL.Entities
{
    public class Transaction
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid Wallet_Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTimeOffset DateTime { get; set; }
    }
}
