using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public OrdersController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<SaleBuyResponseDTO>> BuySellOrdersList([FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _productsService.BuySellOrdersList(authorization));
        }

        [HttpPost("buy")]
        public async Task<ActionResult<String>> Buy(SaleBuyRequestDTO request, [FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _productsService.Buy(request, authorization));
        }

        [HttpPost("sale")]
        public async Task<ActionResult<String>> Sale(SaleBuyRequestDTO request, [FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _productsService.Sale(request, authorization));
        }

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<String>> UpdateOrderById(int id, [FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _productsService.Cancel(id, authorization));
        }
    }
}
