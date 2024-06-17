using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Enums;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;
namespace gestao_de_portfolio.Services
{
    public class ProductsService: IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ISalesBuyManagementService _salesBuyManagementService;
        private readonly IAuthService _authService;
        
        public ProductsService(IProductsRepository productsRepository, ISalesBuyManagementService salesBuyManagementService, IAuthService authService)
        {
            _productsRepository = productsRepository;
            _salesBuyManagementService = salesBuyManagementService;
            _authService = authService;
        }

        public async Task<bool> Create(ProductsModel products, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            if (user == null)
            {
                throw new Exception("Não foi encontrado um usuário, por favor refazer a autenticação");
            }
            return user.Type == UserTypeEnum.admin? await _productsRepository.Create(products): throw new Exception("Não tem permissão para realizar essa ação");
        }

        public async Task<List<ProductsModel>> List()
        {
            return await _productsRepository.List();
        }

        public async Task<ProductsModel> GetById(int id)
        {
            return await _productsRepository.GetByIdAllUsers(id);
        }

        public async Task<bool> Delete(int id, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            if (user == null)
            {
                throw new Exception("Não foi encontrado um usuário, por favor refazer a autenticação");
            }
            ProductsModel product = await _productsRepository.GetById(id);
            return user.Type == UserTypeEnum.admin ? await _productsRepository.Delete(product) : throw new Exception("Não tem permissão para realizar essa ação");
        }

        public async Task<bool> Update(ProductsModel product, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            if (user == null)
            {
                throw new Exception("Não foi encontrado um usuário, por favor refazer a autenticação");
            }

            ProductsModel findProduct = await _productsRepository.GetById(product.Id);
            findProduct.Name = product.Name;
            findProduct.Description = product.Description;
            findProduct.Type = product.Type;
            findProduct.Price = product.Price;
            findProduct.OtherDetails = product.OtherDetails;

            return user.Type == UserTypeEnum.admin ? await _productsRepository.Update(findProduct) : throw new Exception("Não tem permissão para realizar essa ação");
        }

        public async Task<StatusEnum> Buy(SaleBuyRequestDTO request, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            if (user.Money == 0 || user.Money < request.Price * request.Amount)
                throw new Exception("Voce não tem dinheiro o suficiente para realizar uma ordem de compra");
            return await _salesBuyManagementService.Buy(request, user);
        }

        public async Task<StatusEnum> Sale(SaleBuyRequestDTO request, string token) 
        {
            UsersModel user = await _authService.GetUserId(token);
            return await _salesBuyManagementService.Sale(request, user);
        }

        public async Task<List<SaleBuyResponseDTO>> BuySellOrdersList(string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            return await _salesBuyManagementService.List(user);
        }

        public async Task<StatusEnum> Cancel(int id, string token)
        {
            UsersModel user = await _authService.GetUserId(token);
            return await _salesBuyManagementService.UpdateStatus(id, user);
        }
    }
}
