using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Chat action service abstraction.
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        ///  Add new message in message pool
        /// </summary>
        /// <param name="messageToPost"></param>
        /// <returns>True if message was posted; False if smth went wrong/returns>
        Task<bool> PostMessageAsync(MessageToPostDTO messageToPost, Guid accountId);

        /// <summary>
        /// Deletes message from message pool(Only admins opportunity)
        /// </summary>
        /// <param name="id">Unique identifier of message</param>
        /// <returns>True if message was deleted; False if it's impossible</returns>
        Task<bool> DeleteMessageAsync(Guid id);
    }
}
