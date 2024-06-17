using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestao_de_portfolio.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO login);
        Task<UsersModel> GetUserId(string authorization);
    }
}
