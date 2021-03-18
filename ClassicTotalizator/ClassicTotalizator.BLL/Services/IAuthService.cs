using ClassicTotalizator.BLL.Contracts;
using System;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Account auth service abstraction.
    /// </summary>
    public interface IAuthService
    {

        /// <summary>
        /// Registers user and assigns unique account id.
        /// </summary>
        /// <param name="registerDTO">Contract for registration.</param>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<string> RegisterAsync(AccountRegisterDTO registerDTO);
      
        /// <summary>
        /// Login user and returns jwt token.
        /// </summary>
        /// <param name="loginDTO">User login.</param>
        /// <returns>Returns user account id or <c>null</c> if user wasn't found or password is invalid.</returns>
        Task<string> LoginAsync(AccountLoginDTO loginDTO);

        /// <summary>
        /// Logouts user account from platform
        /// </summary>
        /// <param name="jwt">JWT of current account session</param>
        /// <returns></returns>
        Task<bool> LogoutAsync(string jwt);
    }
}
