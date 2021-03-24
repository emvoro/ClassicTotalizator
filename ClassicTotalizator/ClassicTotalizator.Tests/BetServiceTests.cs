using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Services;
using ClassicTotalizator.BLL.Services.IMPL;
using ClassicTotalizator.DAL.Context;
using ClassicTotalizator.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;

namespace ClassicTotalizator.Tests
{
    public class BetServiceTests
    {
        private IBetService _betService;

        [Fact]
        public async Task GetEventBets_MustReturnNull_If_EventBetsIsEmpty()
        {

            var bets = await _betService.GetEventBets(Guid.NewGuid());

            Assert.Null(bets);
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}