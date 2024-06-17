using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Services.Interfaces
{
    public interface IInvestmentsService
    {
        Task<List<InvestmentResponseDTO>> List(string token);
        Task<InvestmentsModel> GetById(int id, string token);
        Task<InvestmentsModel> GetByIdWithProduct(int id, string token);
    }
}
