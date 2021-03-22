using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string AvatarLink { get; set; }

        public string AccountType { get; set; }

        public DateTimeOffset DOB { get; set; }

        public WalletDTO Wallet { get; set; }
        
        public DateTimeOffset AccountCreationTime { get; set; }
    }
}