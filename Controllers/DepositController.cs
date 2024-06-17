using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult<bool>> AddMoney([FromBody] AddDecreaseMoneyDTO addMoney, [FromHeader(Name = "Authorization")] string authorization)
        {
            var result = await _depositService.AddMoney(addMoney, authorization);
            return Ok(result);
        }

        [HttpPost("decrease")]
        [Authorize]
        public async Task<ActionResult<bool>> DecreaseMoney([FromBody] AddDecreaseMoneyDTO addMoney, [FromHeader(Name = "Authorization")] string authorization)
        {
            var result = await _depositService.DecreaseMoney(addMoney, authorization);
            return Ok(result);
        }
    }
}
