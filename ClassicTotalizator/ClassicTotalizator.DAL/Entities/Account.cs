using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ClassicTotalizator.DAL.Entities
{
    public class Account : IdentityUser
    {
        [Key]
        [Required]
        public new Guid Id { get; set; }
        [Required]
        public override string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must me minimum 6 symbols length.")]
        public override string PasswordHash { get; set; }
        public string AvatarLink { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public DateTimeOffset Dob { get; set; }
        //public Wallet Wallet { get; set; }
        public DateTimeOffset AccountCreationTime { get; set; }
        public ICollection<Bet> BetsHistory { get; set; }
    }
}
