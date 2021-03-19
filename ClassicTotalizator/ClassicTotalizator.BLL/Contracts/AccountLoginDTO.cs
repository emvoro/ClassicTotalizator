using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts
{
    /// <summary>
    /// The contract coming from UI, for authentication purposes
    /// </summary>
    public class AccountLoginDTO
    {
        /// <summary>
        /// Login(user's mail) to check for the possibility of entering the platform
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        
        /// <summary>
        ///  Current user password for entering the platform
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
