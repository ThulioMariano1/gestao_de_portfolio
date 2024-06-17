using gestao_de_portfolio.Data;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestao_de_portfolio.Repository
{
    public class SalesBuyManagementRepository : ISalesBuyManagementRepository
    {
        private readonly PortfolioDBContext _dbContext;
        public SalesBuyManagementRepository(PortfolioDBContext portfolioDBContext)
        {
            _dbContext = portfolioDBContext;
        }        
        public async Task<List<SaleBuyResponseDTO>> List(UsersModel user)
        {
            var query = from sale in _dbContext.SalesBuyManagement
                        join product in _dbContext.Products
                        on sale.ProductId equals product.Id
                        where sale.UserId == user.Id
                        select new SaleBuyResponseDTO
                        {
                            Id = sale.Id,
                            BuyDate = sale.BuyDate,
                            SaleDate = sale.SaleDate,
                            Price = sale.Price,
                            Amount = sale.Amount,
                            Status = sale.Status,
                            Product = product,
                        };
            return await query.ToListAsync();
        }
        public async Task<SalesBuyManagementModel> GetById(int id, UsersModel user)
        {
            //SalesBuyManagementModel? salesBuy = await _dbContext.SalesBuyManagement.FindAsync(id);
            SalesBuyManagementModel? salesBuy = await _dbContext.SalesBuyManagement
                .Where(s => s.UserId == user.Id && s.Id == id)
                .FirstAsync();
            if (salesBuy == null)
            {
                throw new Exception($"A Compra ou Venda do ID {id} não foi localizado");
            }
            return salesBuy;
        }

        public async Task<SaleBuyResponseDTO> GetByIdWithProduct(int id, UsersModel user)
        {

            var query = from sale in _dbContext.SalesBuyManagement
                        join product in _dbContext.Products
                        on sale.ProductId equals product.Id
                        where sale.Id == id && sale.UserId == user.Id
                        select new SaleBuyResponseDTO
                        {
                            Id = sale.Id,
                            BuyDate = sale.BuyDate,
                            SaleDate = sale.SaleDate,
                            Price = sale.Price,
                            Amount = sale.Amount,
                            Status = sale.Status,
                            Product = product,
                        };
            SaleBuyResponseDTO? salesBuy = await query.FirstAsync();
            if (salesBuy == null)
            {
                throw new Exception($"A Compra ou Venda do ID {id} não foi localizado");
            }
            return salesBuy;
        }

        public async Task<bool> Create(SalesBuyManagementModel saleBuy)
        {
            InvestmentsModel? investment = await _dbContext.Investments
                .Where( w=> w.UserId == saleBuy.UserId && w.ProductId == saleBuy.ProductId)
                .FirstOrDefaultAsync();
            if (investment != null)
            {
                if(investment.Amount < saleBuy.Amount && saleBuy.Status == StatusEnum.VendaRegistrada)
                {
                    throw new Exception("Não é possivel vender esta quantidade, é maior que a quantidade que possui");
                }
                await _dbContext.SalesBuyManagement.AddAsync(saleBuy);
                await _dbContext.SaveChangesAsync();
                return true;
                
            }
            throw new Exception("Não é possivel criar um compra ou venda, você não possui este produto");
        }
        public async Task<bool> Update(SalesBuyManagementModel saleBuy, UsersModel user)
        {
            //_dbContext.SalesBuyManagement.Update(saleBuy);
            await _dbContext.SalesBuyManagement.Where(x => x.UserId == user.Id && saleBuy.Id == x.Id)
                .ExecuteUpdateAsync(up => up
                    .SetProperty(b => b.Status, saleBuy.Status)
                    .SetProperty(b => b.Price, saleBuy.Price)
                    .SetProperty(b => b.Amount, saleBuy.Amount)
                    .SetProperty(b => b.BuyDate, saleBuy.BuyDate)
                    .SetProperty(b => b.SaleDate, saleBuy.SaleDate));
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(SalesBuyManagementModel saleBuy, UsersModel user)
        {
            var result = await _dbContext.SalesBuyManagement.Where(x => x.Id == saleBuy.Id && user.Id == x.UserId).FirstAsync();
            if(result == null)
            {
                throw new Exception("Item não encontrado");
            }
            _dbContext.SalesBuyManagement.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
