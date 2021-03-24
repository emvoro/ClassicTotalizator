using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.API.Services;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with user accounts.
    /// </summary>
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAccountService _accountService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="logger">Logger</param>
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Get all accounts for admin
        /// </summary>
        /// <returns>All accounts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountForAdminDTO>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();

            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

        /// <summary>
        /// Get user account for chat view
        /// </summary>
        /// <returns>All accounts</returns>
        [HttpGet("account")]
        public async Task<ActionResult<IEnumerable<AccountInfoDTO>>> GetAccountForChat()
        {
            var accountId = ClaimsIdentityService.GetIdFromToken(User);

            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            try
            {
                var account = await _accountService.GetById(accountId);

                if (account == null)
                    return BadRequest("Invalid user account!");

                return Ok(account);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return Conflict();
            }
        }
    }
}