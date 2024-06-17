using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductsModel>> List();
        Task<ProductsModel> GetById(int id);
        Task<bool> Create(ProductsModel products, string token);
        Task<bool> Update(ProductsModel products, string token);
        Task<bool> Delete(int id, string token);
        Task<List<SaleBuyResponseDTO>> BuySellOrdersList(string token);
        Task<StatusEnum> Buy(SaleBuyRequestDTO saleBuyRequest, string token);
        Task<StatusEnum> Sale(SaleBuyRequestDTO saleBuyRequest, string token);
        Task<StatusEnum> Cancel(int id, string token);
    }
}
