using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

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
        /// <param name="registerDTO"></param>
        /// <returns>Jwt token with encrypted [snth to add]</returns>       //ToDo: smth inserted in jwt
        string GenerateJwt(Account registerDTO, string securityKey); // ToDo: delte plug and set user

        /*/// <summary>
        /// Creates new Jwt token
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>Jwt token with encrypted [snth to add]</returns>       //ToDo: smth inserted in jwt
        string GenerateJwt(AccountLoginDTO loginDTO, string se);*/
    }
}
