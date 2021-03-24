using ClassicTotalizator.API.Services;
using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using ClassicTotalizator.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClassicTotalizator.API.Controllers
{
    /// <summary>
    /// This controller contains operations with bets for logged in users.
    /// </summary>
    [ApiController]
    [Route("api/v1/wallet")]
    [Authorize(Roles = Roles.User)]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        private readonly ILogger<ChatController> _logger;

        /// <summary>
        /// Bet controller Constructor
        /// </summary>
        /// <param name="betService">Bet service</param>
        /// <param name="logger">Logger</param>
        public ChatController(IChatService chatService,
            ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }


        /// <summary>
        /// Get current message list (Be careful, this api is not yet implemented)
        /// </summary>
        /// <returns>List of all messages now in chat(100)</returns>
        [HttpGet("getCurrentMessages")]
        public async Task<ActionResult<CurrentChatMessagesDTO>> GetMessagesInChat()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add message to message pool
        /// </summary>
        /// <param name="toPostDTO">Text dto that user trying to post</param>
        /// <returns>True if message was posted or false if smth went wrong</returns>
        [HttpPost("postMessage")]
        public async Task<ActionResult<bool>> PostMessageInChat([FromBody] MessageToPostDTO toPostDTO)
        {
            if (!ModelState.IsValid || toPostDTO == null)
            {
                _logger.LogWarning("Model invalid!");
                return BadRequest($"You tried to post invalid message");
            }
            var accountId = ClaimsIdentityService.GetIdFromToken(User);
            try
            {
                var posted = await _chatService.PostMessageAsync(toPostDTO, accountId);

                if (!posted)
                    return BadRequest("The message was not memorized");

                return Ok(posted);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest($"You tried to post invalid message");
            }
        }

        /// <summary>
        /// Only for admins permissions(Deleting Messages from chat)
        /// </summary>
        /// <param name="messageId">Deletes message from chat action</param>
        /// <returns>True if message was deleted</returns>
        [HttpDelete("deleteMessage/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<bool>> DeleteMessage([FromRoute] Guid messageId)
        {
            try
            {
                var deleted = await _chatService.DeleteMessageAsync(messageId);

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