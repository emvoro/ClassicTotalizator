using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class AccountMapper
    {
        public static Account ToAccount(AccountRegisterDTO registerDTO)
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
    }
}
