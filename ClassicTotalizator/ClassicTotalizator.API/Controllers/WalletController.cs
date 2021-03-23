using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
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
        private readonly ILogger<WalletController> _logger;

        private readonly IWalletService _walletService;

        /// <summary>
        /// Wallet Controller Constructor
        /// </summary>
        /// <param name="walletService">Wallet service</param>
        /// <param name="logger">Logger</param>
        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        /// <summary>
        /// Get account wallet.
        /// </summary>
        /// <returns>Account wallet</returns>
        [HttpGet]
        public async Task<ActionResult<WalletDTO>> GetWalletByAccId()
        {
            var accountId = GetIdFromToken();
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");
            
            var wallet = await _walletService.GetWalletByAccId(accountId);
            if (wallet == null)
                return NotFound();
            
            return Ok(wallet);
        }

        /// <summary>
        /// Get account transaction history.
        /// </summary>
        /// <returns>Transaction history</returns>
        [HttpGet("transactionHistory")]
        public async Task<ActionResult<IEnumerable<TransactionWithTimeDTO>>> GetTransactionHistory()
        {
            var accountId = GetIdFromToken();
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            var transactionHistoryByAccId = await _walletService.GetTransactionHistoryByAccId(accountId);
            if (transactionHistoryByAccId == null)
                return NotFound();

            return Ok(transactionHistoryByAccId);
        }

        /// <summary>
        /// Make a transaction : deposit or withdraw
        /// </summary>
        /// <returns>Wallet</returns>
        [HttpPost("transaction")]
        public async Task<ActionResult<WalletDTO>> AddTransaction([FromBody] TransactionDTO transactionDto)
        {
            if (transactionDto == null)
                return BadRequest("Invalid parameter!");

            var accountId = GetIdFromToken();
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            try
            {
                var wallet = await _walletService.Transaction(accountId, transactionDto);
                if (wallet == null)
                    return BadRequest("Invalid transaction!");

                return Ok(wallet);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return Conflict();
            }
        }
        
        private Guid GetIdFromToken()
        {
            if (!(User.Identity is ClaimsIdentity identity))
                return Guid.Empty;
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return Guid.Empty;

            return accountId;
        }
    }
}