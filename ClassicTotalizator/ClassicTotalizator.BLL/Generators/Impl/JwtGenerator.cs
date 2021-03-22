using ClassicTotalizator.BLL.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.BLL.Generators.IMPL
{
    public class JwtGenerator : IJwtGenerator
    {
        public string GenerateJwt(AccountDTO account, string securityKey)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.AccountType)
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                 notBefore: DateTime.UtcNow,
                 claims: identity.Claims,
                 expires: DateTime.UtcNow.AddDays(1d),
                 signingCredentials: credentials
                 );


            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
