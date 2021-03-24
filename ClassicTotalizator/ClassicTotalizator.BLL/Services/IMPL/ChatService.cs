using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services.IMPL
{
    public class ChatService : IChatService
    {
        private readonly DatabaseContext _context;

        public ChatService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMessageAsync(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                throw new ArgumentException();

            var message = _context.Messages.FirstOrDefault(msg => msg.Id == id);

            if (message == null)
                return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostMessageAsync(MessageToPostDTO messageToPost, Guid accountId)
        {
            if (messageToPost == null || string.IsNullOrEmpty(accountId.ToString()))
                throw new ArgumentNullException();
            if (messageToPost.CreationTime > DateTimeOffset.UtcNow)
                return false;

            var newMessage = ChatMessageMapper.Map(messageToPost);
            newMessage.Id = Guid.NewGuid();
            newMessage.Account_Id = accountId;
            newMessage.Time = DateTimeOffset.UtcNow;
            newMessage.Account = await _context.Accounts.FindAsync(accountId);

            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
