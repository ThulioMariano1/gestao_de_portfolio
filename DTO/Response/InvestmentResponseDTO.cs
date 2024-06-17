using gestao_de_portfolio.Models;

namespace gestao_de_portfolio.DTO.Response
{
    public class InvestmentResponseDTO: InvestmentsModel
    {
        public ProductsModel? Product { get; set; }
    }
}
