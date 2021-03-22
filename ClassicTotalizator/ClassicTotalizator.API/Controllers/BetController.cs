using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.BetDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// Controller for bets, can use only user after login.
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
        /// Get bets on account
        /// </summary>
        /// <returns>Bets on account</returns>
        [HttpGet("account")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetBetsByAccId()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return BadRequest();

            var bets = await _betService.GetBetsByAccId(accountId);
            if (bets == null)
                return NotFound();

            return Ok(bets);
        }

        /// <summary>
        /// Getting event bet's
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns>Event bet's</returns>
        [HttpGet("event/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEventBets([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            
            var bets = await _betService.GetEventBets(id);
            if (bets == null)
                return NotFound();

            return Ok(bets);
        }

        /// <summary>
        /// Add bet
        /// </summary>
        /// <param name="bet">New bet from user</param>
        /// <returns>Status code, ok if bet done, something another if not</returns>
        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> AddBet([FromBody] BetDTO bet)
        {
            if (!ModelState.IsValid || bet == null)
                return BadRequest();
            
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
                return BadRequest();
            
            var stringId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(stringId, out var accountId))
                return BadRequest();

            try
            {
                bet.Account_Id = accountId;
                
                if (await _betService.AddBet(bet))
                {
                    return Ok();
                }
                
                return BadRequest();
            }
            catch (ArgumentNullException)
            {
                _logger.LogWarning("ArgumentNullException! Try to add null obj in BetController!");
                return BadRequest();
            }
        }
    }
}