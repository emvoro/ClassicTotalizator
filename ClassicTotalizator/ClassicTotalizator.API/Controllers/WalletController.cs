using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// API for wallet
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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
        /// <param name="id">Account id</param>
        /// <returns>Account wallet</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDTO>> GetWalletByAccId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var wallet = await _walletService.GetWalletByAccId(id);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
        }

        /// <summary>
        /// Action give account transaction history
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Transaction history</returns>
        [HttpGet("transactionHistory/{id}")]
        public async Task<ActionResult> GetTransactionHistory([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var wallet = await _walletService.GetWalletByAccId(id);
            if (wallet == null)
                return NotFound();

            return Ok(wallet);
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

            try
            {
                var wallet = await _walletService.Transaction(transactionDto);
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