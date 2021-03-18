using ClassicTotalizator.BLL.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ClassicTotalizator.BLL.Generators.IMPL
{
    public class JwtGenerator : IJwtGenerator
    {
        private string _securityKey { get; set; }

        public string GenerateJwt(AccountRegisterDTO registerDto, string securityKey)
        {
            _securityKey = securityKey;
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenSecurityKey = Encoding.UTF8.GetBytes(_securityKey);

            return null;
        }
    }
}
