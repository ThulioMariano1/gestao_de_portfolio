using gestao_de_portfolio.Models;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductsModel>>> List([FromHeader(Name = "Authorization")] string authorization) {
            List<ProductsModel> products = await _productsService.List();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductsModel>>> GetById(int id)
        {
            ProductsModel products = await _productsService.GetById(id);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(ProductsModel products, [FromHeader(Name = "Authorization")] string authorization) {
            bool result =  await _productsService.Create(products, authorization);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(ProductsModel products, [FromHeader(Name = "Authorization")] string authorization) {
            bool result = await _productsService.Update(products, authorization);
            return Ok(result); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id, [FromHeader(Name = "Authorization")] string authorization) {
            bool result = await _productsService.Delete(id, authorization);
            return Ok(result); 
        }
    }
}
