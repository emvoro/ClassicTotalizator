using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class AccountMapper
    {
        public static Account Map(AccountDTO obj)
        {
            return obj == null
                ? null
                : new Account
                {
                    Id = obj.Id,
                    Email = obj.Email,
                    Username = obj.Username,
                    PasswordHash = obj.PasswordHash,
                    AccountCreationTime = obj.AccountCreationTime,
                    AccountType = obj.AccountType,
                    AvatarLink = obj.AvatarLink,
                    DOB = obj.DOB,
                    Wallet = WalletMapping.Map(obj.Wallet)
                };
        }

        public static AccountDTO Map(Account obj)
        {
            return obj == null
                ? null
                : new AccountDTO
                {
                    Id = obj.Id,
                    Email = obj.Email,
                    Username = obj.Username,
                    PasswordHash = obj.PasswordHash,
                    AccountCreationTime = obj.AccountCreationTime,
                    AccountType = obj.AccountType,
                    AvatarLink = obj.AvatarLink,
                    DOB = obj.DOB,
                    Wallet = WalletMapping.Map(obj.Wallet)
                };
        }
        
        public static AccountForAdminDTO MapForAdmin(Account obj)
        {
            if (obj == null)
                return null;

            var accountForAdmin = new AccountForAdminDTO
            {
                Email = obj.Email,
                Username = obj.Username,
                DOB = obj.DOB
            };

            if (obj.Wallet != null)
                accountForAdmin.WalletAmount = obj.Wallet.Amount;

            return accountForAdmin;
        }

        public static AccountInfoDTO MapForChatInfo(Account obj)
        {
            if (obj == null)
                return null;

            var accountInfo = new AccountInfoDTO
            {
                Id = obj.Id,
                Username = obj.Username,
                AvatarLink = obj.AvatarLink
            };

            return accountInfo;
        }
    }
}
