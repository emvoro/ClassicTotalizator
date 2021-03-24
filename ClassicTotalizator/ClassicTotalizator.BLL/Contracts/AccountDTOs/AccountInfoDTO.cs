using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    /// <summary>
    /// The contract exists to show user info in chat
    /// </summary>
    public class AccountInfoDTO
    {
        /// <summary>
        /// Account Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Account Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The link to auto-generated user avatar
        /// </summary>
        public string AvatarLink { get; set; }
    }
}
