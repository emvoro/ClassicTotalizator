using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.SportDTOs;
using System;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with sports.
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : Controller
    {
        private readonly ILogger<SportsController> _logger;

        private readonly ISportService _sportService;

        /// <summary>
        /// Events Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sportService"></param>
        public SportsController(ILogger<SportsController> logger,
            ISportService sportService)
        {
            _logger = logger;
            _sportService = sportService;
        }

        /// <summary>
        /// Get all sports.
        /// </summary>
        /// <returns>List of all possible sports on the platform</returns>
        [HttpGet]
        public async Task<ActionResult<SportsDTO>> GetAllSportsInPlatform()
        {
            var sports = await _sportService.GetCurrentListOfSportsAsync();
            if (sports == null)
                return NotFound();

            return Ok(sports);
        }

        /// <summary>
        /// Adds new sport.
        /// </summary>
        /// <returns>Sport DTO</returns>
        [HttpPost]
        public async Task<ActionResult<SportDTO>> AddSport([FromBody] SportDTO sportDto)
        {
            if (!ModelState.IsValid || sportDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try 
            {
                var createdSport = await _sportService.AddAsync(sportDto);
                
                return Ok(createdSport);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes sport.
        /// </summary>
        /// <returns>Deleting state.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSportSport([FromRoute] int id)
        {
            if (!await _sportService.DeleteSportAsync(id))
                return NotFound(false);

            return Ok(true);
        }
    }
}
