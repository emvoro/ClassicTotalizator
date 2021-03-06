using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using ClassicTotalizator.BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.DAL.Repositories;

namespace ClassicTotalizator.BLL.Services.Impl
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _repository;

        private readonly IAccountRepository _accountRepository;

        public ChatService(IMessageRepository repository,
            IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> DeleteMessageAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException();

            await _repository.RemoveByIdAsync(id);

            return true;
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync()
        {

            var messages = await _repository.GetLastMessagesAsync();
            var messagesDto = messages.Select(ChatMessageMapper.Map).ToList();

            foreach (var message in messagesDto)
            {
                var account = await _accountRepository.GetByIdAsync(message.Account_Id);
                message.Username = account.Username;
                message.AvatarLink = account.AvatarLink;
            }

            return messagesDto;
        }

        public async Task<bool> PostMessageAsync(MessageToPostDTO messageToPost, Guid accountId)
        {
            if (messageToPost == null || accountId == Guid.Empty)
                throw new ArgumentNullException();

            var newMessage = ChatMessageMapper.Map(messageToPost);
            newMessage.Id = Guid.NewGuid();
            newMessage.Account_Id = accountId;
            newMessage.Time = DateTimeOffset.UtcNow;

            await _repository.AddAsync(newMessage);

            return true;
        }
    }
}