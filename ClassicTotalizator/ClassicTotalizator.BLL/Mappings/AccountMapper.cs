﻿using ClassicTotalizator.BLL.Contracts;
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

        public static Account Map(Contracts.AccountDTO obj)
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
                    Wallet = obj.Wallet
                };
        }
        
        public static Contracts.AccountDTO Map(Account obj)
        {
            return obj == null
                ? null
                : new Contracts.AccountDTO
                {
                    Id = obj.Id,
                    Email = obj.Email,
                    PasswordHash = obj.PasswordHash,
                    AccountCreationTime = obj.AccountCreationTime,
                    AccountType = obj.AccountType,
                    AvatarLink = obj.AvatarLink,
                    DOB = obj.DOB,
                    Wallet = obj.Wallet
                };
        }
    }
}
