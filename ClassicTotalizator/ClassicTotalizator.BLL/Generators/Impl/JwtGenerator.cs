using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClassicTotalizator.BLL.Contracts;

namespace ClassicTotalizator.BLL.Generators.IMPL
{
    public class JwtGenerator : IJwtGenerator
    {
        private string _securityKey { get; set; }

        public string GenerateJwt(AccountDTO account, string securityKey)
        {
            _securityKey = securityKey;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.AccountType),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "USER")
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));

            var credentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1d),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContext = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenContext);
        }
    }
}
