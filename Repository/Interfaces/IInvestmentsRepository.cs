using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Repository.Interfaces
{
    public interface IInvestmentsRepository
    {
        Task<List<InvestmentResponseDTO>> List(UsersModel user);
        Task<InvestmentsModel> GetById(int id, UsersModel user);
        Task<InvestmentResponseDTO> GetByIdWithProduct(int id, UsersModel user);
        Task<bool> Create(InvestmentsModel investment);
        Task<bool> Update(InvestmentsModel investment);
        Task<bool> Delete(InvestmentsModel investment);
    }
}
