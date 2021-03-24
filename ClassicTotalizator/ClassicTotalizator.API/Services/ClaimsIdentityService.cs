using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Services
{
    /// <summary>
    /// Service that gets an account id from token.
    /// </summary>
    public static class ClaimsIdentityService
    {
        /// <summary>
        /// Method that gets an account id from token.
        /// </summary>
        /// <param name="user">User claims principal.</param>
        public static Guid GetIdFromToken(ClaimsPrincipal user)
        {
            if (user.Identity is not ClaimsIdentity identity)
                return Guid.Empty;

            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(stringId, out var accountId))
                return Guid.Empty;

            return accountId;
        }
    }
}
