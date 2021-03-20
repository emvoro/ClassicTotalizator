using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassicTotalizator.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "USER")]
    [Route("api/v1/bet")]
    public class BetController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult> GetById([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] BetDto bet)
        {
            throw new NotImplementedException();
        }
    }
}