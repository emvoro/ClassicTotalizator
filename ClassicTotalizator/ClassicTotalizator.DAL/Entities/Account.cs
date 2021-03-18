using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Account
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must me minimum 6 symbols length.")]
        public string PasswordHash { get; set; }

        public string AvatarLink { get; set; }

        [Required]
        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        //public Wallet Wallet { get; set; }

        public ICollection<Bet> BetsHistory { get; set; }
    }
}
