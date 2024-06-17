using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Models;
using System.Runtime.InteropServices;

namespace gestao_de_portfolio.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<UsersModel> Login(LoginDTO login);
        Task<bool> Create(CreateUserDTO user);
        Task<bool> Update(UsersModel user);

        Task<UsersModel> Get(int id);

    }
}
