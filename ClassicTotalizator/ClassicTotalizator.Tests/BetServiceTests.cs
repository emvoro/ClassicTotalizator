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

        }
    }
}