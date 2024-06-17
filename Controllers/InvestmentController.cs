using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentsService _investmentsService;
        public InvestmentController(IInvestmentsService investmentsService)
        {
            _investmentsService = investmentsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvestmentResponseDTO>>> List([FromHeader(Name = "Authorization")] string authorization) {
            return Ok(await _investmentsService.List(authorization));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentsModel>> GetById(int id, [FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _investmentsService.GetById(id, authorization));
        }
        [HttpGet("{id}/context")]
        public async Task<ActionResult<InvestmentResponseDTO>> GetByIdWithProduct(int id, [FromHeader(Name = "Authorization")] string authorization)
        {
            return Ok(await _investmentsService.GetByIdWithProduct(id, authorization));
        }
    }
}
