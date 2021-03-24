using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Services
{
    public static class ClaimsIdentityService
    {
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
