using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class AccountMapper
    {
        public static Account Map(AccountRegisterDTO accountRegisterDTO)
        {
            return accountRegisterDTO == null
                ? null
                : new Account
                {
                    Username = accountRegisterDTO.Username,
                    Email = accountRegisterDTO.Email,
                    PasswordHash = accountRegisterDTO.Password,
                    DOB = accountRegisterDTO.DOB,
                    AccountCreationTime = accountRegisterDTO.AccountCreationTime
                };
        }

        public static Account Map(AccountLoginDTO accountLoginDTO)
        {
            return accountLoginDTO == null
                ? null
                : new Account
                {
                    Email = accountLoginDTO.Login,
                    PasswordHash = accountLoginDTO.Password
                };
        }

        public static Account Map(AccountDTO accountDTO)
        {
            return accountDTO == null
                ? null
                : new Account
                {
                    Id = accountDTO.Id,
                    Email = accountDTO.Email,
                    Username = accountDTO.Username,
                    PasswordHash = accountDTO.PasswordHash,
                    AccountCreationTime = accountDTO.AccountCreationTime,
                    AccountType = accountDTO.AccountType,
                    AvatarLink = accountDTO.AvatarLink,
                    DOB = accountDTO.DOB,
                    Wallet = WalletMapping.Map(accountDTO.Wallet)
                };
        }

        public static AccountDTO Map(Account account)
        {
            return account == null
                ? null
                : new AccountDTO
                {
                    Id = account.Id,
                    Email = account.Email,
                    Username = account.Username,
                    PasswordHash = account.PasswordHash,
                    AccountCreationTime = account.AccountCreationTime,
                    AccountType = account.AccountType,
                    AvatarLink = account.AvatarLink,
                    DOB = account.DOB,
                    Wallet = WalletMapping.Map(account.Wallet)
                };
        }
        
        public static AccountForAdminDTO MapForAdmin(Account account)
        {
            if (account == null)
                return null;

            var accountForAdmin = new AccountForAdminDTO
            {
                Email = account.Email,
                Username = account.Username,
                DOB = account.DOB
            };

            if (account.Wallet != null)
                accountForAdmin.WalletAmount = account.Wallet.Amount;

            return accountForAdmin;
        }

        public static AccountInfoDTO MapForChatInfo(Account account)
        {
            if (account == null)
                return null;

            var accountInfo = new AccountInfoDTO
            {
                Id = account.Id,
                Username = account.Username,
                AvatarLink = account.AvatarLink
            };

            return accountInfo;
        }
    }
}
