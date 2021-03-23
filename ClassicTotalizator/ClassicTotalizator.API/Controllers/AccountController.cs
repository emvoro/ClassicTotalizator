using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.AccountDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with user accounts.
    /// </summary>
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService">Account service</param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
    }
}