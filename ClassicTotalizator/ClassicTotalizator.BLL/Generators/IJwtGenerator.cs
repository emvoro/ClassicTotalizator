using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.BLL.Generators
{
    /// <summary>
    /// Jwt generation abstraction
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Creates new Jwt token
        /// </summary>
        /// <param name="account">Account</param>
        /// <param name="securityKey">Security key for token</param>
        /// <returns>Jwt token with encrypted [snth to add]</returns>
        string GenerateJwt(AccountDTO account, string securityKey);
    }
}
