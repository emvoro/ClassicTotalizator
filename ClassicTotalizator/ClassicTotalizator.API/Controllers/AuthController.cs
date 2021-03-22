﻿using ClassicTotalizator.API.Options;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    ///  This controller is used to register and login the user on the platform
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
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
        /// <param name="registerDTO">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        public async Task<ActionResult<JwtDTO>> RegisterAsync([FromBody] AccountRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid || registerDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var token = await _authService.RegisterAsync(registerDTO);
                var jwtReturnedDTO = new JwtDTO
                {
                    JwtString = token
                };
                
                return CheckTokenAndReturn(jwtReturnedDTO, "Register failed!");
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Login action.
        /// </summary>
        /// <param name="loginDTO">Requested dto for login on platform</param>
        /// <returns>Returns JWT</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("Login")]
        public async Task<ActionResult<JwtDTO>> LoginAsync(AccountLoginDTO loginDTO)
        {
            if (!ModelState.IsValid || loginDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            var token = await _authService.LoginAsync(loginDTO);
            var jwtReturnedDTO = new JwtDTO
            {
                JwtString = token
            };
            
            return CheckTokenAndReturn(jwtReturnedDTO, "Login failed!");
        }

        private ActionResult<JwtDTO> CheckTokenAndReturn(JwtDTO jwt, string message)
        {
            if (!string.IsNullOrEmpty(jwt.JwtString)) 
                return Ok(jwt);
            
            if (!string.IsNullOrEmpty(message)) 
                _logger.LogWarning(message);
                
            return NotFound();
        }
    }
}
