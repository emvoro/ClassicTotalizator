﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.Impl;
using ClassicTotalizator.DAL.Entities;
using ClassicTotalizator.DAL.Repositories;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class SportServiceTests
    {
        private ISportService _sportService;

        private readonly Mock<IRepository<Sport>> _mockRepository = new Mock<IRepository<Sport>>();

        private readonly Mock<ISportRepository> _repository = new Mock<ISportRepository>();

        [Fact]
        public async Task AddSport_Throws_ArgumentNullException()
        {
            _sportService = new SportService(null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _sportService.AddAsync(null));
        }

        [Fact]
        public async Task AddSport_Returns_SportObject_IfAddedSuccessfully()
        {

        }

        [Fact]
        public async Task GetCurrentListOfSports_Returns_EmptyList_If_RepositoryIsEmpty()
        {
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Sport>());

            _sportService = new SportService(_repository.Object);

            var sports = await _sportService.GetCurrentListOfSportsAsync();

            Assert.Empty(sports.Sports);
        }

        [Fact]
        public async Task DeleteSport_ReturnFalse_If_SportNotFound()
        {
            var id = 11111;

            _repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Sport)null);

            _sportService = new SportService(_repository.Object);

            Assert.False(await _sportService.DeleteSportAsync(id));
        }

        [Fact]
        public async Task DeleteSport_ReturnTrue_If_SportIsFound()
        {
            var id = 1;

            _repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Sport());

            _sportService = new SportService(_repository.Object);

            Assert.True(await _sportService.DeleteSportAsync(id));
        }

        [Fact]
        public async Task DeleteSport_ReturnFalse_If_IdIsNotValid()
        {
            var id = -1;

            _repository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new Sport());

            _sportService = new SportService(_repository.Object);

            Assert.False(await _sportService.DeleteSportAsync(id));
        }

        public static IEnumerable<object[]> Sports()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Sport
                    {
                        
                    }
                },
                new object[]
                {
                    new Sport
                    {
                        
                    }
                },
                new object[]
                {
                    new Sport
                    {
                        
                    }
                },
            };
        }
    }
}
