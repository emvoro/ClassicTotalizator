﻿using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration Configuration;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
            _authService.SecurityKey = Configuration.GetSection("AuthKey").GetValue<string>("Secret");
        }

        /// <summary>
        /// Registration action.
        /// </summary>
        /// <param name="registerDto">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync([FromBody] AccountRegisterDTO registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            var token = await _authService.RegisterAsync(registerDto);
            return CheckTokenAndReturn(token, "Register failed!");
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
            if (!ModelState.IsValid || loginDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            var token = await _authService.LoginAsync(loginDto);
            return CheckTokenAndReturn(token, "Login failed!");
        }

        //Here we must check token for admins purposes
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
