using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    public class AccountForAdminDTO
    {
        public string Email { get; set; }

        public string Username { get; set; }
        
        public DateTimeOffset DOB { get; set; }
        
        public decimal WalletAmount { get; set; }
    }
}