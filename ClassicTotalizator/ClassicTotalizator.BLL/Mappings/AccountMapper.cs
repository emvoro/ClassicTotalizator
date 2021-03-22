using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class AccountMapper
    {
        public static Account Map(AccountRegisterDTO registerDTO)
        {
            return registerDTO == null
                ? null
                : new Account
                {
                    Email = registerDTO.Email,
                    PasswordHash = registerDTO.Password,
                    DOB = registerDTO.DOB,
                    AccountCreationTime = registerDTO.AccountCreationTime
                };
        }
        
        public static Account Map(AccountLoginDTO loginDTO)
        {
            return loginDTO == null
                ? null
                : new Account
                {
                    Email = loginDTO.Login,
                    PasswordHash = loginDTO.Password
                };
        }

        public static Account Map(AccountDTO obj)
        {
            return obj == null
                ? null
                : new Account
                {
                    Id = obj.Id,
                    Email = obj.Email,
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
                    PasswordHash = obj.PasswordHash,
                    AccountCreationTime = obj.AccountCreationTime,
                    AccountType = obj.AccountType,
                    AvatarLink = obj.AvatarLink,
                    DOB = obj.DOB,
                    Wallet = WalletMapping.Map(obj.Wallet)
                };
        }
    }
}
