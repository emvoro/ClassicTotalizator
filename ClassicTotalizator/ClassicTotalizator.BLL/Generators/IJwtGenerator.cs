using ClassicTotalizator.BLL.Contracts;

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
        /// <param name="account"></param>
        /// <returns>Jwt token with encrypted [snth to add]</returns>       //ToDo: smth inserted in jwt
        string GenerateJwt(AccountDTO account, string securityKey); // ToDo: delte plug and set user

        /*/// <summary>
        /// Creates new Jwt token
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>Jwt token with encrypted [snth to add]</returns>       //ToDo: smth inserted in jwt
        string GenerateJwt(AccountLoginDTO loginDTO, string se);*/
    }
}
