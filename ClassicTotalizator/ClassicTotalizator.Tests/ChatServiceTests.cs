using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace ClassicTotalizator.Tests
{
    public class ChatServiceTests
    {
        private IChatService _chatService;

        private readonly Mock<IMessageRepository> _messageRepository = new Mock<IMessageRepository>();

        private readonly Mock<IAccountRepository> _accountRepository = new Mock<IAccountRepository>();

        [Fact]
        public async Task DeleteMessageAsync_CheckIfArgumentExceptionWasThrown_ExceptionWasThrown()
        {
            _chatService = new ChatService(null, null);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _chatService.DeleteMessageAsync(Guid.Empty));

        }

        [Fact]
        public async Task DeleteMessageAsync_CheckIfMessageWasDeletedSuccessfully_ReturnsTrue()
        {
            var id = Guid.NewGuid();

            var message = new Message
            {
                Id = id,
                Account_Id = id,
                Text = "Test message",
                Time = DateTimeOffset.UtcNow
            };

            _messageRepository.Setup(x => x.AddAsync(message)).Returns(Task.CompletedTask);
            _messageRepository.Setup(x => x.RemoveByIdAsync(id)).Returns(Task.CompletedTask);

            _chatService = new ChatService(_messageRepository.Object, null);

            var isMessageDeleted = await _chatService.DeleteMessageAsync(id);

            Assert.True(isMessageDeleted);
        }

        [Fact]
        public async Task PostMessageAsync_CheckIfExceptionWasThrownWithIvalidGuid_ArgumentNullExceptionWasThrown()
        {
            _chatService = new ChatService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await _chatService.PostMessageAsync(new MessageToPostDTO { Text = "SomeText" }, Guid.Empty));
        }

        [Fact]
        public async Task PostMessageAsync_CheckIfExceptionWasThrownWithIvalidPostedMessage_ExceptionWasThrown()
        {
            _chatService = new ChatService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async ()
                => await _chatService.PostMessageAsync(null, Guid.NewGuid()));
        }

        [Fact]
        public async Task PostMessageAsync_CheckIfMessageWasPostedSuccessfully_TrueWasReturned()
        {
            var messageToPost = new MessageToPostDTO
            {
                Text = "Some text"
            };

            _messageRepository.Setup(x => x.AddAsync(ChatMessageMapper.Map(messageToPost))).Returns(Task.CompletedTask);

            _chatService = new ChatService(_messageRepository.Object, null);

            var isMessagePosted = await _chatService.PostMessageAsync(messageToPost, Guid.NewGuid());

            Assert.True(isMessagePosted);
        }

        [Fact]
        public async Task GetMessagesAsync_CheckIfMethodReturnsEmptyListIfZeroMessagesInDb_EmptyListWasReturned()
        {

            _messageRepository.Setup(x => x.GetLastMessagesAsync()).ReturnsAsync(new List<Message>());

            _chatService = new ChatService(_messageRepository.Object, null);

            var currentChatLsit = await _chatService.GetMessagesAsync();

            Assert.Empty(currentChatLsit);
        }

        [Fact]
        public async Task GetMessagesAsync_CheckIfMethodReturnsListOfCurrentMessagesInChat_ListWasReturned()
        {
            var id = Guid.NewGuid();
            var messagesInList = new List<Message>
            {
                new Message
                {
                    Id =Guid.NewGuid(),
                    Account_Id = id,
                    Text ="Default text"
                },
                new Message
                {
                    Id =Guid.NewGuid(),
                    Account_Id = id,
                    Text ="Default message"
                }
            };
            _messageRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(messagesInList);

            messagesInList.ForEach(message =>
            _accountRepository.Setup(x => x.GetByIdAsync(message.Account_Id)).ReturnsAsync(new Account
            {
                Id = id,
                Username = "Tst",
                AvatarLink =" This is Link"
            })) ;

            _chatService = new ChatService(_messageRepository.Object, _accountRepository.Object);

            var currentMessagesList = await _chatService.GetMessagesAsync();

            var chatMessage = currentMessagesList.FirstOrDefault( msg => msg.Text == "Default message");

            var expectedMessage = messagesInList.FirstOrDefault(msg => msg.Text == "Default message");

            Assert.Equal(expectedMessage.Text, chatMessage.Text);
            Assert.Equal(id, chatMessage.Account_Id);
        }
    }
}
