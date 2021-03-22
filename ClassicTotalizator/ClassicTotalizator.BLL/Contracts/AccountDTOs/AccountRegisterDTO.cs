using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    /// <summary>
    ///  The contract coming from UI, for the registration of the user
    /// </summary>
    public class AccountRegisterDTO
    {
        public AccountRegisterDTO()
        {
            AccountCreationTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// User mail by which the user will be remembered
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@".+@.+\..+")]
        public string Email { get; set; }

        /// <summary>
        /// User password which will be allowed to enter the account
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        ///  User's date of birth for the ability to play on the platform
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public DateTimeOffset DOB { get; set; }
        
        /// <summary>
        ///  Account creation request date
        /// </summary>
        public DateTimeOffset AccountCreationTime { get; set; }
    }
}
