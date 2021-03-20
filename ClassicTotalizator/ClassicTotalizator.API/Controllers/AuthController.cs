using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    ///  This controller is used to register and login the user on the platform
    /// </summary>
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        private readonly ILogger<AuthController> _logger;
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authService">Auth service</param>
        /// <param name="logger">Logger</param>
        /// <param name="configuration">Configuration</param>
        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger, IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            Configuration = configuration;
            _authService.SecurityKey = Configuration.GetSection("AuthKey").GetValue<string>("Secret");
        }

        /// <summary>
        /// Registration action.
        /// </summary>
        /// <param name="registerDto">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RegisterAsync([FromBody] AccountRegisterDTO registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                return CheckTokenAndReturn(token, "Register failed!");
            }
            catch (ArgumentNullException)
            {
                _logger.LogWarning("Argument null exception! Try to add null object in AuthController!");
                return BadRequest();
            }
        }

        /// <summary>
        /// Login action.
        /// </summary>
        /// <param name="loginDto">Requested dto for login on platform</param>
        /// <returns>Returns JWT</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginAsync(AccountLoginDTO loginDto)
        {
            if (!ModelState.IsValid || loginDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            var token = await _authService.LoginAsync(loginDto);
            return CheckTokenAndReturn(token, "Login failed!");
        }

        private ActionResult<string> CheckTokenAndReturn(string token, string message)
        {
            if (!string.IsNullOrEmpty(token)) 
                return Ok(token);
            
            if (!string.IsNullOrEmpty(message)) 
                _logger.LogWarning(message);
                
            return NotFound();
        }
    }
}
