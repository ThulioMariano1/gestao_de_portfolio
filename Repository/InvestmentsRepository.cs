using gestao_de_portfolio.Data;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestao_de_portfolio.Repository
{
    public class InvestmentsRepository : IInvestmentsRepository
    {
        private readonly PortfolioDBContext _dbContext;
        public InvestmentsRepository(PortfolioDBContext portfolioDBContext)
        {
            _dbContext = portfolioDBContext;
        }
        public async Task<List<InvestmentResponseDTO>> List(UsersModel user)
        {
            try
            {
                var query = from investment in _dbContext.Investments
                            join product in _dbContext.Products
                            on investment.ProductId equals product.Id
                            where investment.UserId == user.Id
                            select new InvestmentResponseDTO
                            {
                                Id = investment.Id,
                                Product = product,
                                Amount = investment.Amount,
                                Price = investment.Price,
                                DateAcquisition = investment.DateAcquisition,
                                //SaleDate = investment.SaleDate
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Não existe dados para gerar uma lista", ex);
            }
        }
        public async Task<InvestmentsModel> GetById(int id, UsersModel user)
        {
            InvestmentsModel? investment = await _dbContext.Investments.Where(x => x.Id == id && user.Id == x.UserId).FirstAsync();
            if (investment == null)
            {
                throw new Exception($"O investimento do ID: {id} não existe");
            }
            return investment;
        }

        public async Task<InvestmentResponseDTO> GetByIdWithProduct(int id, UsersModel user)
        {
            try
            {
                var query = from investment in _dbContext.Investments
                            join product in _dbContext.Products
                            on investment.ProductId equals product.Id
                            where investment.Id == id && investment.UserId == user.Id
                            select new InvestmentResponseDTO
                            {
                                Id = investment.Id,
                                Product = product,
                                Amount = investment.Amount,
                                Price = investment.Price,
                                DateAcquisition = investment.DateAcquisition,
                            };
                InvestmentResponseDTO? salesBuy = await query.FirstAsync();
                return salesBuy;
            }
            catch (Exception ignore)
            {
                throw new Exception($"A Compra ou Venda do ID {id} não foi localizado", ignore);
            }
        }

        public async Task<bool> Create(InvestmentsModel investment)
        {
            await _dbContext.Investments.AddAsync(investment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(InvestmentsModel investment)
        {
            _dbContext.Investments.Update(investment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(InvestmentsModel investment)
        {
            _dbContext.Investments.Remove(investment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
