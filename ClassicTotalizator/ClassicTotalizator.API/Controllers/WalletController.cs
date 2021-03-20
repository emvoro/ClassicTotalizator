using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="walletService">Wallet service</param>
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
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
    }
}