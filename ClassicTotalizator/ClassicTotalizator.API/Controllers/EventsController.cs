using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{

    /// <summary>
    /// THIS PART ACTUALLY NOT IMPLEMENTED DONT TOUCH THIS
    /// </summary>
    [Authorize(Roles ="ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get participants action
        /// </summary>
        /// <returns>Collection of all registered participants for [CURRENT SPORT]</returns>        //Here we need to consume type of sport
        [HttpGet("participants")]
        public async Task<ActionResult<IEnumerable<ParticipantsDTO>>> GetAllParticipantsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// All possible outcomes for events action
        /// </summary>
        /// <returns>List of all possible outcomes</returns>
        [HttpGet("outcomes")]
        public async Task<IEnumerable<int>> GepAllPossibleResults() => new List<int> { 0, 1, 2 };

        /// <summary>
        /// Get all sports action
        /// </summary>
        /// <returns>List of all possible sports on the platform</returns>
        [HttpGet("sports")]
        public async Task<IEnumerable<SportsDTO>> GetAllSportsInPlatform()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register event action
        /// </summary>
        /// <param name="registerDTO">Registered model of event that will be saved in platformed and showed for the audience</param>
        /// <returns>Builded whole event for next configuretion</returns>
        [HttpPost("addEvent")]
        public async Task<ActionResult<EventDTO>> AddEventToLine(EventRegisterDTO registerDTO) // Add consuming event Dto
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting events pool action
        /// </summary>
        /// <returns>List of all handled events on the platfrom</returns>
        [HttpGet("getEventsPool")]
        public async Task<ActionResult<EventsPoolDTO>> GetAllEvents()
        {
            throw new NotImplementedException();
        }
    }
}
