using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Repository.Interfaces
{
    public interface ISalesBuyManagementRepository
    {
        Task<List<SaleBuyResponseDTO>> List(UsersModel user);
        Task<SalesBuyManagementModel> GetById(int id, UsersModel user);
        Task<SaleBuyResponseDTO> GetByIdWithProduct(int id, UsersModel user);
        Task<bool> Create(SalesBuyManagementModel investment);
        Task<bool> Update(SalesBuyManagementModel investment, UsersModel user);
        Task<bool> Delete(SalesBuyManagementModel saleBuy, UsersModel user);
    }
}
