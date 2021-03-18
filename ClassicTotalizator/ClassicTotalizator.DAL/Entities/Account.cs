using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ClassicTotalizator.DAL.Entities
{
    public class Account : IdentityUser
    {
        [Key]
        public new Guid Id { get; set; }

        public override string Email { get; set; }
        
        [MinLength(6, ErrorMessage = "Password must me minimum 6 symbols length.")]
        public override string PasswordHash { get; set; }
        
        public string AvatarLink { get; set; }

        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        public Wallet Wallet { get; set; }
        
        public DateTimeOffset AccountCreationTime { get; set; }

        public ICollection<Bet> BetsHistory { get; set; }

        public Account()
        {
            BetsHistory = new List<Bet>();
        }
    }
}
