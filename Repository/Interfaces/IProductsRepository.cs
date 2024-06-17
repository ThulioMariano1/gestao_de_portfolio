using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.Repository.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<ProductsModel>> List();
        Task<bool> Create(ProductsModel product);
        Task<bool> Update(ProductsModel product);
        Task<bool> Delete(ProductsModel product);
        Task<ProductsModel> GetByIdAllUsers(int id);
        Task<ProductsModel> GetById(int id);
    }
}
