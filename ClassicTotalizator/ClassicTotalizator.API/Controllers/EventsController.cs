using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.EventDTOs;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with events.
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        
        private readonly IEventService _eventService;

        /// <summary>
        /// Events Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventService"></param>
        public EventsController(ILogger<EventsController> logger,
            IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns>List of all events.</returns>
        [HttpGet]
        public async Task<ActionResult<EventsFeedDTO>> GetAllEvents()
        {
            var events = await _eventService.GetEventsAsync();
            if (events == null)
                return NotFound();

            return Ok(events);
        }

        /// <summary>
        /// Get current line.
        /// </summary>
        /// <returns>List of all current active events</returns>
        [HttpGet("feed")]
        [AllowAnonymous]
        public async Task<ActionResult<EventsFeedDTO>> GetCurrentLine()
        {
            var currentLine = await _eventService.GetCurrentLineOfEventsAsync();
            if (currentLine == null)
                return NotFound();

            return Ok(currentLine);
        }

        /// <summary>
        /// Get all possible outcomes for events.
        /// </summary>
        /// <returns>List of all possible outcomes</returns>
        [HttpGet("outcomes")]
        public IActionResult GetAllPossibleOutcomes()
        {
            return Ok(new OutcomesDTO());
        }

        /// <summary>
        /// Get event by id.
        /// </summary>
        /// <returns>Event by id</returns>
        [HttpGet("{id}")]
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
        /// Get event preview by id
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns>Event preview</returns>
        [HttpGet("{id}/preview")]
        [AllowAnonymous]
        public async Task<ActionResult<EventPreviewDTO>> GetEventPreviewById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"This guid: [{id}] not valid!");
            
            var eventPreview = await _eventService.GetEventPreviewAsync(id);
            if (eventPreview == null)
                return NotFound($"Event by this id [{id}] was not found");

            return Ok(eventPreview);
        }

        /// <summary>
        /// Add new event.
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPost]
        public async Task<ActionResult<EventDTO>> AddEvent([FromBody] EventRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid || registerDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var createdEvent = await _eventService.CreateEventAsync(registerDTO);
                if (createdEvent == null)
                    return BadRequest();

                return Ok(createdEvent);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return Forbid();
            }
        }


        /// <summary>
        /// Close event.
        /// </summary>
        /// <param name="finishedEvent">Event</param>
        /// <returns>Bool value, true id closed, another - false</returns>
        [HttpPost("finish")]
        public async Task<ActionResult<bool>> FinishEvent([FromBody] FinishedEventDTO finishedEvent)
        {
            if (!ModelState.IsValid || finishedEvent == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var finished = await _eventService.FinishEventAsync(finishedEvent);
                if (!finished)
                    BadRequest();

                return Ok(finished);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning("Argument null exception. " + e.ParamName);
                return BadRequest();
            }
        }


        /// <summary>
        /// Edit event.
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPut("{id}/edit")]
        public async Task<ActionResult<EventDTO>> PatchEvent([FromBody] EditedEventDTO eventDTO)
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
        /// Deletes event BUT be careful:)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEvent([FromRoute]Guid id)
        {
            try
            {
                var deleted = await _eventService.DeleteEventAsync(id);
                if (!deleted)
                    return NotFound(deleted);

                return Ok(deleted);
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }
    }
}