using System;
using System.Threading.Tasks;
using ClassicTotalizator.API.Services;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with bets for logged in users.
    /// </summary>
    [ApiController]
    [Route("api/v1/bet")]
    public class BetController : ControllerBase
    {
        private readonly IBetService _betService;
        
        private readonly ILogger<BetController> _logger;

        /// <summary>
        /// Bet controller Constructor
        /// </summary>
        /// <param name="betService">Bet service</param>
        /// <param name="logger">Logger</param>
        public BetController(IBetService betService, ILogger<BetController> logger)
        {
            _betService = betService;
            _logger = logger;
        }

        /// <summary>
        /// Get bets on account.
        /// </summary>
        /// <returns>Bets on account</returns>
        [HttpGet("account")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> GetBetsByAccId()
        {
            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            var bets = await _betService.GetBetsByAccId(accountId);
            if (bets == null)
                return NotFound("Bets not found!");

            return Ok(bets);
        }

        /// <summary>
        /// Getting event bet's
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns>Event bet's</returns>
        [HttpGet("event/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> GetEventBets([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id is empty!");
            
            var bets = await _betService.GetEventBets(id);
            if (bets == null)
                return NotFound("Bets not found!");

            return Ok(bets);
        }

        /// <summary>
        /// Add bet
        /// </summary>
        /// <param name="bet">New bet from user</param>
        /// <returns>Status code, ok if bet done, something another if not</returns>
        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> AddBet([FromBody] BetNewDTO bet)
        {
            if (!ModelState.IsValid || bet == null)
                return BadRequest("Model is invalid!");

            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            if (accountId == Guid.Empty)
                return BadRequest("Token value is invalid!");

            try
            {
                if (await _betService.AddBet(bet, accountId))
                {
                    return Ok();
                }
                
                return BadRequest();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest("Argument null exception!");
            }
        }
    }
}