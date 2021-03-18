using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accounts;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAccountService accounts,
            ILogger<AuthController> logger)
        {
            _accounts = accounts;
            _logger = logger;
        }
        /// <summary>
        /// Registration action.
        /// </summary>
        /// <param name="registerDTO">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RegisterAsync(AccountRegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
