using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
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
        /// <param name="registerDto">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync(AccountRegisterDTO registerDto)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Login action.
        /// </summary>
        /// <param name="loginDto">Requested dto for login on platform</param>
        /// <returns>Returns JWT</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAsync(AccountLoginDTO loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
