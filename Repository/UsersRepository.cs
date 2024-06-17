using gestao_de_portfolio.Data;
using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace gestao_de_portfolio.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PortfolioDBContext _dbContext;
        public UsersRepository(PortfolioDBContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<bool> Create(CreateUserDTO user)
        {
            if (EmailValido(user.Email))
            {
                var result = await _dbContext.Users.AnyAsync(x => x.Email == user.Email);
                if (!result)
                {
                    UsersModel usersModel = new UsersModel(user);
                    await _dbContext.Users.AddAsync(usersModel);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                throw new Exception("Email incorreto, favor verificar");
            }
            throw new Exception("Email já cadastrado, favor verificar");
        }

        public async Task<UsersModel> Get(int id)
        {
            UsersModel? user = await _dbContext.Users.FindAsync(id);
            return user ?? throw new Exception($"Usuário não localizado para o ID: {id}");
        }

        public async Task<UsersModel> Login(LoginDTO login) 
        {
            if (EmailValido(login.Email))
            {
                var result = await _dbContext.Users.Where(x => x.Email == login.Email && x.PassWord == login.Password).FirstAsync();
                return result ?? throw new Exception("Email ou senha incorreta");
            }
            throw new Exception("Email incorreto, favor verificar");

        }

        public async Task<bool> Update(UsersModel user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        private bool EmailValido(string email)
        {
            string padrao = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex regex = new Regex(padrao);
            return regex.IsMatch(email);
        }
    }
}
