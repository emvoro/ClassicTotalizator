using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    /// <summary>
    /// The contract coming from UI, for authentication purposes
    /// </summary>
    public class AccountDTO
    {
        /// <summary>
        /// Account id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Account username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Account email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashcode of user password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// The link to auto-generated user avatar
        /// </summary>
        public string AvatarLink { get; set; }

        /// <summary>
        /// The type of user account
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// User's Date of Birth
        /// </summary>
        public DateTimeOffset DOB { get; set; }

        /// <summary>
        /// User's wallet
        /// </summary>
        public WalletDTO Wallet { get; set; }

        /// <summary>
        /// Date and Time of account creation
        /// </summary>
        public DateTimeOffset AccountCreationTime { get; set; }
    }
}