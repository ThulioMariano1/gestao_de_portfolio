using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            string? token = await _authService.Login(model);
            if(token.IsNullOrEmpty()){
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }
            return Ok(new { token });

        }
    }
}
