using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> Create([FromBody] CreateUserDTO users)
        {
            var result = await _usersRepository.Create(users);
            return Ok(result);
        }
    }
}
