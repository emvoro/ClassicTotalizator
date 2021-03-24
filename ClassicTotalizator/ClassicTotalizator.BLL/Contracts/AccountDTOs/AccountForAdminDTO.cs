using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    /// <summary>
    /// The contract exists to transfer user data to administrators
    /// </summary>
    public class AccountForAdminDTO
    {
        /// <summary>
        /// Account email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Account Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User Date of Birth
        /// </summary>
        public DateTimeOffset DOB { get; set; }

        /// <summary>
        /// Amount of money in the user's wallet
        /// </summary>
        public decimal WalletAmount { get; set; }
    }
}