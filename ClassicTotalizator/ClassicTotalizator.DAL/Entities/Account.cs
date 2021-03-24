using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.DAL.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        
        [MinLength(6, ErrorMessage = "Password must be minimum 6 symbols length.")]
        public string PasswordHash { get; set; }
        
        public string AvatarLink { get; set; }

        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        public Wallet Wallet { get; set; }
        
        public DateTimeOffset AccountCreationTime { get; set; }

        public ICollection<Bet> BetsHistory { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Account()
        {
            BetsHistory = new List<Bet>();
            Messages = new List<Message>();
        }
    }
}
