using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Models;
using gestao_de_portfolio.Repository.Interfaces;
using gestao_de_portfolio.Services.Interfaces;

namespace gestao_de_portfolio.Services
{
    public class InvestmentsService: IInvestmentsService
    {
        private readonly IInvestmentsRepository _investmentsRepository;
        private readonly IAuthService _authService;
        public InvestmentsService(IInvestmentsRepository investmentsRepository, IAuthService authService) 
        {
            _investmentsRepository = investmentsRepository;
            _authService = authService;
        }

        public async Task<InvestmentsModel> GetById(int id, string token)
        {
            UsersModel usersModel = await _authService.GetUserId(token);
            return await _investmentsRepository.GetById(id, usersModel);
        }

        public async Task<InvestmentsModel> GetByIdWithProduct(int id, string token)
        {
            UsersModel usersModel = await _authService.GetUserId(token);
            return await _investmentsRepository.GetByIdWithProduct(id, usersModel);
        }

        public async Task<List<InvestmentResponseDTO>> List(string token)
        {
            UsersModel usersModel = await _authService.GetUserId(token);
            return await _investmentsRepository.List(usersModel);
        }
    }
}
