using gestao_de_portfolio.Models;
using System.Text.Json.Serialization;

namespace gestao_de_portfolio.DTO.Response
{
    public class SaleBuyResponseDTO : SalesBuyManagementModel
    {
        // [JsonIgnore]
        public new int UserId { get; set; }

        // [JsonIgnore]
        public new int ProductId { get; set; }
        public ProductsModel? Product { get; set; }
    }
}
