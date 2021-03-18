﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    [Table("Account")]
    class Account
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must me minimum 6 symbols length.")]
        public string PasswordHash { get; set; }

        public string AvatarLink { get; set; }

        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        public Wallet Wallet { get; set; }

        public ICollection<Bet> BetsHistory { get; set; }

        public Account()
        {
            BetsHistory = new List<Bet>();
        }
    }
}
