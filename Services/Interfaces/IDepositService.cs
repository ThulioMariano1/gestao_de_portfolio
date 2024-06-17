using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Repository.Interfaces;

namespace gestao_de_portfolio.Services.Interfaces
{
    public interface IDepositService
    {
        Task<bool> DecreaseMoney(AddDecreaseMoneyDTO amount, string token);
        Task<bool> AddMoney(AddDecreaseMoneyDTO amount, string token);

    }
}
