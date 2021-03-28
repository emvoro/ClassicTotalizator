﻿using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.SportDTOs;

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
        public async Task<ActionResult<SportDTO>> AddSport([FromBody] SportDTO sportDTO)
        {
            if (!ModelState.IsValid || sportDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            var createdSport = await _sportService.AddAsync(sportDTO);

            return Ok(createdSport);
        }

        /// <summary>
        /// Deletes sport.
        /// </summary>
        /// <returns>Deleting state.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSportSport([FromRoute] int id)
        {
            var deleted = await _sportService.DeleteSportAsync(id);
            if (!deleted)
                return NotFound(deleted);

            return Ok(deleted);
        }
    }
}
