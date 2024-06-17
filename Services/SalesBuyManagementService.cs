using Azure.Core;
using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;

namespace gestao_de_portfolio.Services
{
    public class SalesBuyManagementService : ISalesBuyManagementService
    {
        private readonly ISalesBuyManagementRepository _salesBuyManagementRepository;
        public SalesBuyManagementService(ISalesBuyManagementRepository salesBuyManagementRepository)
        {
            _salesBuyManagementRepository = salesBuyManagementRepository;
        }
        public async Task<List<SaleBuyResponseDTO>> List(UsersModel user)
        {
            return await _salesBuyManagementRepository.List(user);
        }
        public async Task<SalesBuyManagementModel> GetById(int id, UsersModel user)
        {
            return await _salesBuyManagementRepository.GetById(id, user);
        }

        public async Task<StatusEnum> Buy(SaleBuyRequestDTO request, UsersModel user) 
        {
            SalesBuyManagementModel model = new SalesBuyManagementModel();
            model.ProductId = request.ProductId;
            model.BuyDate = new DateTime().Date;
            model.ProductId = request.ProductId;
            model.Amount = request.Amount;
            model.Price = request.Price;
            model.Status = StatusEnum.CompraRegistrada;
            model.UserId = user.Id;

            await _salesBuyManagementRepository.Create(model);
            return StatusEnum.CompraRegistrada;
        }
        public async Task<StatusEnum> Sale(SaleBuyRequestDTO request, UsersModel user)
        {
            SalesBuyManagementModel model = new SalesBuyManagementModel();
            model.ProductId = request.ProductId;
            model.BuyDate = new DateTime().Date;
            model.ProductId = request.ProductId;
            model.Amount = request.Amount;
            model.Price = request.Price;
            model.Status = StatusEnum.VendaRegistrada;
            model.UserId = user.Id;

            await _salesBuyManagementRepository.Create(model);
            return StatusEnum.VendaRegistrada;
        }

        public async Task<bool> Delete(int id, UsersModel user) 
        {
            SalesBuyManagementModel salesBuy = await _salesBuyManagementRepository.GetById(id, user);
            return await _salesBuyManagementRepository.Delete(salesBuy, user);
        }

        public async Task<StatusEnum> UpdateStatus(int id, UsersModel user)
        {
            SalesBuyManagementModel? salesBuy = await _salesBuyManagementRepository.GetById(id, user);
            salesBuy.Status = salesBuy.Status switch
            {
                StatusEnum.CompraRegistrada => StatusEnum.CompraCancelada,
                StatusEnum.VendaRegistrada => StatusEnum.VendaCancelada,
                StatusEnum.CompraExecutadaEmPartes or 
                StatusEnum.VendaExecutadaEmPartes => throw new Exception("Não é possivel cancelar, já esta em andamento"),
                StatusEnum.CompraFinalizada or 
                StatusEnum.VendaFinalizada => throw new Exception("Não é possivel cancelar, já esta finalizada"),
                _ => throw new Exception("Não é possivel cancelar, já esta cancelada"),
            };
            await _salesBuyManagementRepository.Update(salesBuy, user);
            return salesBuy.Status;
        }
    }
}
