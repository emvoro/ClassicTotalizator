using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Transaction
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
