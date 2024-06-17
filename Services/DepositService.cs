using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestao_de_portfolio.Services
{
    public class DepositService: IDepositService
    {
        private readonly IAuthService _authService;
        private readonly IUsersRepository _usersRepository;

        public DepositService(IAuthService authService, IUsersRepository usersRepository)
        {
            _authService = authService;
            _usersRepository = usersRepository;
        }

        public async Task<bool> DecreaseMoney(AddDecreaseMoneyDTO money, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            if (user.Money - money.Price >= 0)
            {
                user.Money = user.Money - money.Price;
                await _usersRepository.Update(user);
                return true;
            }
            throw new Exception("Não é possivel sacar essa quantidade, por favor verificar o valor");
        }

        public async Task<bool> AddMoney(AddDecreaseMoneyDTO money, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            user.Money = user.Money + money.Price;
            await _usersRepository.Update(user);
            return true;
        }
    }
}
