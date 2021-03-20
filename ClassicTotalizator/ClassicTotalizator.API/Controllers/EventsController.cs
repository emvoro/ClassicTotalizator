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
        private readonly IEventService _eventService;
        private readonly IParticipantsService _participantsService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventService"></param>
        public EventsController(ILogger<EventsController> logger,
            IEventService eventService,
            IParticipantsService participantsService)
        {
            _logger = logger;
            _eventService = eventService;
            _participantsService = participantsService;
        }

        /// <summary>
        /// Get participants action
        /// </summary>
        /// <returns>Collection of all registered participants for [CURRENT SPORT]</returns>        //Here we need to consume type of sport
        [HttpGet("participants")]
        public async Task<ActionResult<IEnumerable<ParticipantsDTO>>> GetAllParticipantsAsync()
        {
            var participants = await _participantsService.GetAllParticipantsAsync();
            if (participants == null)
                return NotFound();
            return Ok(participants);
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
        /// Returns whole list of all created events
        /// </summary>
        /// <returns>List of all</returns>
        [HttpGet("getEventsPool")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finding event by thats id action
        /// </summary>
        /// <returns>Event with this id</returns>
        [HttpGet("getById")]
        public async Task<ActionResult<EventDTO>> GetEventById()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>List of all possible sports on the platform</returns>
        [HttpGet("feed")]
        public async Task<ActionResult<List<EventDTO>>> GetAllUpcomingEvents()
        {
            var events = await _eventService.GetEventsAsync();
            if (events == null)
                return NotFound();

            return Ok(events);
        }
    }
}
