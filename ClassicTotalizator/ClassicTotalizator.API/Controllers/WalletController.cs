using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// API for wallet
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v1/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        private readonly ILogger<WalletController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="walletService">Wallet service</param>
        /// <param name="logger">Logger</param>
        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        /// <summary>
        /// Action give account wallet
        /// </summary>
        /// <returns>Account wallet</returns>
        [HttpGet]
        public async Task<ActionResult<WalletDTO>> GetWalletByAccId()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return BadRequest();

            var wallet = await _walletService.GetWalletByAccId(accountId);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }

        /// <summary>
        /// Action give account transaction history
        /// </summary>
        /// <returns>Transaction history</returns>
        [HttpGet("transactionHistory")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionHistory()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return BadRequest();

            var transactionHistoryByAccId = await _walletService.GetTransactionHistoryByAccId(accountId);
            if (transactionHistoryByAccId == null)
                return NotFound();

            return Ok(transactionHistoryByAccId);
        }

        /// <summary>
        /// Transaction, withdraw or deposit
        /// </summary>
        /// <returns>Wallet</returns>
        [HttpPost("transaction")]
        public async Task<ActionResult<WalletDTO>> AddTransaction([FromBody] TransactionDTO transactionDto)
        {
            if (transactionDto == null)
                return BadRequest();

            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return BadRequest();

            try
            {
                var wallet = await _walletService.Transaction(accountId, transactionDto);
                if (wallet == null)
                    return BadRequest();

                return Ok(wallet);
            }
            catch (ArgumentNullException)
            {
                _logger.LogWarning("ArgumentNullException! Wallet controller, method AddTransaction, transaction is null!");
                return Conflict();
            }
        }
    }
}