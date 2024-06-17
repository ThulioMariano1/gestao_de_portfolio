using gestao_de_portfolio.Data;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestao_de_portfolio.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly PortfolioDBContext _dbContext;
        public ProductsRepository(PortfolioDBContext portfolioDBContext) {
            _dbContext = portfolioDBContext;
        }
        public async Task<List<ProductsModel>> List()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<ProductsModel> GetById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception($"Produto para o ID: {id} não foi encontrado");
            }
            return product;
        }

        public async Task<bool> Create(ProductsModel product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ProductsModel product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(ProductsModel product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProductsModel> GetByIdAllUsers(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception($"Produto para o ID: {id} não foi encontrado");
            }
            return product;
        }
    }
}
