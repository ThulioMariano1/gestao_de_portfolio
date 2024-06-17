using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Services.Interfaces
{
    public interface ISalesBuyManagementService
    {
        Task<List<SaleBuyResponseDTO>> List(UsersModel user);
        Task<StatusEnum> Buy(SaleBuyRequestDTO saleBuy, UsersModel user);
        Task<StatusEnum> Sale(SaleBuyRequestDTO saleBuy, UsersModel user);
        Task<StatusEnum> UpdateStatus(int id, UsersModel user);
        Task<bool> Delete(int id, UsersModel user);
        Task<SalesBuyManagementModel> GetById(int id, UsersModel user);
    }
}
