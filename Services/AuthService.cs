using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gestao_de_portfolio.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;
        public AuthService(IConfiguration configuration, IUsersRepository usersRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
        }
        public async Task<string> Login(LoginDTO login)
        {
            UsersModel user = await _usersRepository.Login(login);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim("identifier", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"])),
                    Issuer = _configuration["JwtSettings:Issuer"],
                    Audience = _configuration["JwtSettings:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            throw new Exception("Email ou senha incorreta");
        }

        public async Task<UsersModel> GetUserId(string authorization)
        {
            string newAuthorization = authorization.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(newAuthorization);
            var tokenS = handler.ReadToken(newAuthorization) as JwtSecurityToken;
            int identifier = 0;
            foreach (var claim in tokenS.Claims)
            {
                if(claim.Type == "identifier")
                {
                    identifier = int.Parse(claim.Value);
                }
            }
            if (identifier == 0) { throw new Exception("Não foi possivel localicar o ID do usuário no token"); }

            return await _usersRepository.Get(identifier);
        }
    }
}
