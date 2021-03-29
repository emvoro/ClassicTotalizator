using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;
using ClassicTotalizator.BLL.Mappings;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class ParticipantsServiceTests
    {
        private IParticipantsService _participantsService;

        private readonly Mock<IRepository<Participant>> _repository = new Mock<IRepository<Participant>>();

        private readonly Mock<IParameterRepository> _parameterRepository = new Mock<IParameterRepository>();

        private readonly Mock<IPlayerRepository> _playerRepository = new Mock<IPlayerRepository>();

        [Fact]
        public async Task AddParticipant_Throws_ArgumentNullException()
        {
            _participantsService = new ParticipantsService(null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _participantsService.AddNewParticipantAsync(null));
        }

        [Fact]
        public async Task AddParticipant_Returns_NotNullObject_IfAddedSuccessfully()
        {
            var participant = new ParticipantRegisterDTO
            {
                Name = "Participant",
                Parameters = null,
                PhotoLink = "https",
                Players = null
            };

            _repository.Setup(x => x.AddAsync(ParticipantsMapper.Map(participant))).Returns(Task.CompletedTask);

            _participantsService = new ParticipantsService(_repository.Object, _parameterRepository.Object, _playerRepository.Object);

            var participantAdd = await _participantsService.AddNewParticipantAsync(participant);

            Assert.NotNull(participantAdd);
        }

        [Fact]
        public async Task GetAllParticipantsAsync_Returns_EmptyList_If_RepositoryIsEmpty()
        {
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Participant>());

            _participantsService = new ParticipantsService(_repository.Object, _parameterRepository.Object, _playerRepository.Object);

            var participants = await _participantsService.GetAllParticipantsAsync();

            Assert.Empty(participants.Participants);
        }

        [Fact]
        public async Task GetAllParticipantsAsync_ReturnListWithTwoElements_Because_RepositoryHaveTwoParticipants()
        {
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Participant>
            {
                new Participant(),
                new Participant()
            });

            _participantsService = new ParticipantsService(_repository.Object, _parameterRepository.Object, _playerRepository.Object);

            var participants = await _participantsService.GetAllParticipantsAsync();

            Assert.Equal(2, participants.Participants.ToList().Count);
        }

        [Fact]
        public async Task DeleteParticipant_ReturnFalse_If_ParticipantNotFound()
        {
            var id = Guid.NewGuid();

            _repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Participant)null);

            _participantsService = new ParticipantsService(_repository.Object, _parameterRepository.Object, _playerRepository.Object);

            Assert.False(await _participantsService.DeleteParticipantAsync(id));
        }

        [Fact]
        public async Task DeleteParticipant_ReturnTrue_If_ParticipantIsFound()
        {
            var id = new Guid("08d81915-5af5-448d-b49b-a6f658c379aa");

            _repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Participant());

            _repository.Setup(x => x.RemoveByIdAsync(id));

            _participantsService = new ParticipantsService(_repository.Object, _parameterRepository.Object, _playerRepository.Object);

            Assert.True(await _participantsService.DeleteParticipantAsync(id));
        }
    }
}