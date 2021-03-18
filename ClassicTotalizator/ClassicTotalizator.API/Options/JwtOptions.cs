using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Options
{
    public static class JwtOptions
    {
        /// <summary>
        /// Token issuer (producer).
        /// </summary>
        public static string Issuer { get; set; } = "ClassicTotalizator";
        /// <summary>
        /// Token audience (consumer).
        /// </summary>
        public static string Audience { get; set; } = "UIClient";
        /// <summary>
        /// Token life time.
        /// </summary>
        public static TimeSpan LifeTime { get; set; } = TimeSpan.FromDays(1d);
        /// <summary>
        /// Require HTTPS.
        /// </summary>
        public static bool RequireHttps { get; set; } = false;
    }
}
