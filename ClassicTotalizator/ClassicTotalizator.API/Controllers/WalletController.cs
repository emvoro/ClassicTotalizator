using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.API.Services;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.TransactionDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with wallet.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
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
        /// Get account wallet by id.
        /// </summary>
        /// <returns>Account wallet</returns>
        [HttpGet]
        public async Task<ActionResult<WalletDTO>> GetWalletByAccId()
        {
            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            var wallet = await _walletService.GetWalletByAccIdAsync(accountId);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }

        /// <summary>
        /// Get account transaction history.
        /// </summary>
        /// <returns>Transaction history</returns>
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<TransactionWithTimeDTO>>> GetTransactionHistory()
        {
            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            var transactionHistoryByAccId = await _walletService.GetTransactionHistoryByAccIdAsync(accountId);
            if (transactionHistoryByAccId == null)
                return NotFound();

            return Ok(transactionHistoryByAccId);
        }

        /// <summary>
        /// Make a transaction : deposit or withdraw
        /// </summary>
        /// <param name="transactionDto">DTO of transaction</param>
        /// <returns>Wallet</returns>
        [HttpPost]
        public async Task<ActionResult<WalletDTO>> AddTransaction([FromBody] TransactionDTO transactionDto)
        {
            if (transactionDto == null)
                return BadRequest("Invalid parameter!");

            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            try
            {
                var wallet = await _walletService.TransactionAsync(accountId, transactionDto);
                if (wallet == null)
                    return BadRequest("Invalid transaction!");

                return Ok(wallet);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }
    }
}