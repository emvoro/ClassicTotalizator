using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with events.
    /// </summary>
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;
        private readonly IParticipantsService _participantsService;

        /// <summary>
        /// Events Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventService"></param>
        /// /// <param name="participantsService"></param>
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
        /// <returns>Collection of all registered participants for [CURRENT SPORT]</returns>
        [HttpGet("participants")]
        public async Task<ActionResult<IEnumerable<ParticipantDTO>>> GetAllParticipantsAsync()
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
        public IEnumerable<string> GetAllPossibleOutcomes() => new List<string> {"W1","X","W2" };
        
        /// <summary>
        /// Get all sports action
        /// </summary>
        /// <returns>List of all possible sports on the platform</returns>
        [HttpGet("sports")]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetAllSportsInPlatform()
        {
            var sports = await _eventService.GetCurrentListOfSports();
            if (sports == null)
                return NotFound();
            return Ok(sports);
        }

        /// <summary>
        /// Returns list of all created events.
        /// </summary>
        /// <returns>List of Events</returns>
        [HttpGet("getEventsPool")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            var eventsHistory = await _eventService.GetCurrentListOfSports();
            if (eventsHistory == null)
                return NotFound();
            return Ok(eventsHistory);
        }

        /// <summary>
        /// Finding event by id.
        /// </summary>
        /// <returns>Event by id</returns>
        [HttpGet("getById")]
        public async Task<ActionResult<EventDTO>> GetEventById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var foundEvent = await _eventService.GetById(id);

            if (foundEvent == null)
                return NotFound();

            return Ok(foundEvent);
        }

        /// <summary>
        /// Creates new event by template.
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPost("createEvent")]
        public async Task<ActionResult<EventDTO>> CreateEventByTemplate([FromRoute] EventRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid || registerDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }
            var createdEvent = await _eventService.CreateEventAsync(registerDTO);
            if (createdEvent == null)
                return BadRequest();

            return Ok(createdEvent);
        }

        /// <summary>
        /// Edites event.
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPatch("patchEvent")]
        public async Task<ActionResult<EventDTO>> EditEvent([FromBody] EventDTO eventDTO)
        {
            if (!ModelState.IsValid || eventDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }
            var editedEvent = await _eventService.EditEventAsync(eventDTO);
            if (editedEvent == null)
                return BadRequest();

            return Ok(editedEvent);
        }

        /// <summary>
        /// Gets all events.
        /// </summary>
        /// <returns>List of all upcoming events.</returns>
        [HttpGet("feed")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EventDTO>>> GetAllUpcomingEvents()
        {
            var events = await _eventService.GetEventsAsync();
            if (events == null)
                return NotFound();

            return Ok(events);
        }
    }
}
