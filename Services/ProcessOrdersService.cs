using gestao_de_portfolio.Data;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace gestao_de_portfolio.Services
{
    public class ProcessOrdersService : IProcessOrdersService
    {
        private readonly PortfolioDBContext _dbContext;
        public ProcessOrdersService(PortfolioDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessOrdersAsync()
        {
            var buyOffers = await _dbContext.SalesBuyManagement
                .Where(w => w.Status == StatusEnum.CompraRegistrada || w.Status == StatusEnum.CompraExecutadaEmPartes)
                .OrderBy(o => o.Id)
                .ThenBy(t => t.Price)
                .ToListAsync();

            var sellOffers = await _dbContext.SalesBuyManagement
                .Where(w => w.Status == StatusEnum.VendaRegistrada || w.Status == StatusEnum.VendaExecutadaEmPartes)
                .OrderBy(o => o.Id)
                .ThenBy(t => t.Price)
                .ToListAsync();

            foreach (SalesBuyManagementModel? buyOffer in buyOffers)
            {
                foreach (SalesBuyManagementModel? sellOffer in sellOffers.Where(so => so.Price == buyOffer.Price 
                        && so.ProductId == buyOffer.ProductId 
                        && so.UserId != buyOffer.UserId)
                    .ToList())
                {
                    if (buyOffer.Amount == 0)
                        break;
                    var quantityToMatch = Math.Min(buyOffer.Amount, sellOffer.Amount);
                    await UpdateInvestment(buyOffer, sellOffer, quantityToMatch);
                    
                    buyOffer.Amount -= quantityToMatch;
                    sellOffer.Amount -= quantityToMatch;

                    buyOffer.Status = buyOffer.Amount == 0 ? StatusEnum.CompraFinalizada : StatusEnum.CompraExecutadaEmPartes;
                    sellOffer.Status = sellOffer.Amount == 0 ? StatusEnum.VendaFinalizada : StatusEnum.VendaExecutadaEmPartes;

                    if (sellOffer.Amount == 0)
                    {
                        _dbContext.SalesBuyManagement.Update(sellOffer);
                        sellOffers.Remove(sellOffer);
                    }

                    if (buyOffer.Amount == 0)
                    {
                        _dbContext.SalesBuyManagement.Update(buyOffer);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateInvestment(SalesBuyManagementModel buyOffer, SalesBuyManagementModel sellOffer, int quantityToMatch)
        {
            InvestmentsModel? buyResult = await _dbContext.Investments
                .Where(w => w.UserId == buyOffer.UserId && w.ProductId == buyOffer.ProductId)
                .FirstOrDefaultAsync();

            InvestmentsModel? sellResult = await _dbContext.Investments
                .Where(w => w.UserId == sellOffer.UserId && w.ProductId == sellOffer.ProductId)
                .FirstOrDefaultAsync();

            await UpdateBuy(buyResult, buyOffer, quantityToMatch);
            await UpdateSell(sellResult, sellOffer, quantityToMatch);
        }

        private async Task UpdateBuy(InvestmentsModel? investment, SalesBuyManagementModel buyOffer, int quantityToMatch)
        {
            if(investment == null)
            {
                InvestmentsModel newInvestment = new InvestmentsModel();
                newInvestment.UserId = buyOffer.UserId;
                newInvestment.ProductId = buyOffer.ProductId;
                newInvestment.Price = ((newInvestment.Price * newInvestment.Amount) 
                    + (buyOffer.Price * quantityToMatch))/ (newInvestment.Amount + quantityToMatch);
                newInvestment.Amount += quantityToMatch;
                newInvestment.DateAcquisition = new DateTime().Date;

                var money = buyOffer.Price * quantityToMatch;
                await UpdateMoneyUser(buyOffer.UserId, - money);

                await _dbContext.Investments.AddAsync(newInvestment);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                investment.Price = ((investment.Price * investment.Amount)
                    + (buyOffer.Price * quantityToMatch)) / (investment.Amount + quantityToMatch);

                investment.Amount += quantityToMatch;

                var money = buyOffer.Price * quantityToMatch;
                await UpdateMoneyUser(buyOffer.UserId, -money);

                _dbContext.Investments.Update(investment);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task UpdateSell(InvestmentsModel? investment, SalesBuyManagementModel sellOffer, int quantityToMatch)
        {
            if (investment != null) 
            {

                var gain = sellOffer.Price * quantityToMatch;
                investment.Amount -= quantityToMatch;

                await UpdateMoneyUser(sellOffer.UserId, gain);
                _dbContext.Investments.Update(investment);
                await _dbContext.SaveChangesAsync();
            }   
        }

        private async Task UpdateMoneyUser(int UserId, double? gain)
        {

            await _dbContext.Users.Where(w => w.Id == UserId)
                .ExecuteUpdateAsync(up => up.SetProperty(sp => sp.Money, sp=> sp.Money + gain ));
            await _dbContext.SaveChangesAsync();    
        }
    }
}
