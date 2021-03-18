using ClassicTotalizator.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicTotalizator.BLL.Generators.IMPL
{
    public class JwtGenerator : IJwtGenerator
    {
        public string GenerateJwt(AccountRegisterDTO registerDTO, string securityKey)
        {
            _securityKey = securityKey;
            var tokenHandler = new JwtSecurityTokenHandler();
        }
        private string _securityKey { get; set; }
    }
}
