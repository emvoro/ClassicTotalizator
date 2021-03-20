using System;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Contracts
{
    public class AccountDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string AvatarLink { get; set; }

        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        public Wallet Wallet { get; set; }
        
        public DateTimeOffset AccountCreationTime { get; set; }
    }
}