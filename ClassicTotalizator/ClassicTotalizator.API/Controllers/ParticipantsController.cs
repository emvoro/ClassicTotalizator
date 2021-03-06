using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with participants.
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : Controller
    {
        private readonly ILogger<ParticipantsController> _logger;

        private readonly IParticipantsService _participantsService;

        /// <summary>
        /// Events Controller Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="participantsService">Participants service</param>
        public ParticipantsController(ILogger<ParticipantsController> logger,
            IParticipantsService participantsService)
        {
            _logger = logger;
            _participantsService = participantsService;
        }

        /// <summary>
        /// Get all participants
        /// </summary>
        /// <returns>Collection of all registered participants for [CURRENT SPORT]</returns>
        [HttpGet]
        public async Task<ActionResult<ParticipantsDTO>> GetAllParticipantsAsync()
        {
            var participants = await _participantsService.GetAllParticipantsAsync();
            if (participants == null)
                return NotFound();

            return Ok(participants);
        }

        /// <summary>
        /// Add new participant
        /// </summary>
        /// <returns>Event DTO</returns>
        [HttpPost]
        public async Task<ActionResult<ParticipantDTO>> AddParticipant([FromBody] ParticipantRegisterDTO registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest();
            }

            try
            {
                var createdParticipant = await _participantsService.AddNewParticipantAsync(registerDto);
                if (createdParticipant == null)
                    return BadRequest();

                return Ok(createdParticipant);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes participant.
        /// </summary>
        /// <returns>Deleting state</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteParticipant([FromRoute] Guid id)
        {
            if (!await _participantsService.DeleteParticipantAsync(id))
                return NotFound(false);

            return Ok(true);
        }
    }
}
