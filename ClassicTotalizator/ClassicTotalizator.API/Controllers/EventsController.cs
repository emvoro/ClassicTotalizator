using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;
using ClassicTotalizator.BLL.Contracts.SportDTOs;

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
        private readonly IParticipantsService _participantsService;
        private readonly ISportService _sportService;

        /// <summary>
        /// Events Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventService"></param>
        /// <param name="participantsService"></param>
        /// <param name="sportService"></param>
        public EventsController(ILogger<EventsController> logger,
            IEventService eventService,
            IParticipantsService participantsService,
            ISportService sportService)
        {
            _logger = logger;
            _eventService = eventService;
            _participantsService = participantsService;
            _sportService = sportService;
        }

        /// <summary>
        /// Get participants action
        /// </summary>
        /// <returns>Collection of all registered participants for [CURRENT SPORT]</returns>
        [HttpGet("participants")]
        public async Task<ActionResult<ParticipantsDTO>> GetAllParticipantsAsync()
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
        public IActionResult GetAllPossibleOutcomes()
        {
            return Ok(new OutcomesDTO());
        }

        /// <summary>
        /// Get all sports action
        /// </summary>
        /// <returns>List of all possible sports on the platform</returns>
        [HttpGet("sports")]
        public async Task<ActionResult<SportsDTO>> GetAllSportsInPlatform()
        {
            var sports = await _eventService.GetCurrentListOfSports();

            if (sports == null)
                return NotFound();

            return Ok(sports);
        }

        /// <summary>
        /// Finding event by id.
        /// </summary>
        /// <returns>Event by id</returns>
        [HttpGet("getById/{id}")]
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
        [Authorize(Roles=Roles.Admin)]
        [HttpPost("createEvent")]
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
        /// Creates new participant by template.
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPost("addParticipant")]
        public async Task<ActionResult<EventDTO>> AddParticipant([FromBody] ParticipantRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid || registerDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var createdParticipant = await _participantsService.AddNewParticipant(registerDTO);

                if (createdParticipant == null)
                    return BadRequest();

                return Ok(createdParticipant);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return Forbid();
            }
        }

        /// <summary>
        /// Adds sport.
        /// </summary>
        /// <returns>Sport DTO</returns>
        [Authorize(Roles=Roles.Admin)]
        [HttpPost("addSport")]
        public async Task<ActionResult<SportDTO>> AddSport([FromBody] SportDTO sportDTO)
        {
            if (!ModelState.IsValid || sportDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }
            var createdSport = await _sportService.Add(sportDTO);

            return Ok(createdSport);
        }

        ///// <summary>
        ///// New participant adding action.
        ///// </summary>
        ///// <returns>True if participant added in database or false if smth went wrongs</returns>
        //[Authorize(Roles = Roles.Admin)]
        //[HttpPost("addParticipant")]
        //public async Task<ActionResult<bool>> AddParticipant([FromBody] ParticipantDTO participantDTO)
        //{
        //    if (!ModelState.IsValid || participantDTO == null)
        //    {
        //        _logger.LogWarning("Model invalid!");
        //        return BadRequest();
        //    }
        //    var participant = await _participantsService.AddNewParticipant(participantDTO);

        //    return Ok(participant);
        //}

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
        /// <returns>List of all events.</returns>
        [HttpGet("getAllEvents")]
        public async Task<ActionResult<List<EventDTO>>> GetAllEvents()
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
        public async Task<ActionResult<EventsDTO>> GetCurrentLine()
        {
            var currentLine = await _eventService.GetCurrentLineOfEvents();
            if (currentLine == null)
                return NotFound();

            return Ok(currentLine);
        }

        [HttpPatch("finishEvent")]
        public async Task<ActionResult<bool>> CloseEvent([FromBody] FinishedEventDTO finishedEvent )
        {
            var finished = await _eventService.FinishEvent(finishedEvent);

            if (!finished)
                BadRequest();

            return Ok(finished);
        }
    }
}