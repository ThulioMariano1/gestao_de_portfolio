
using gestao_de_portfolio.Enums;

namespace gestao_de_portfolio.Models
{
    public class InvestmentsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double? Price { get; set; }
        public DateTime? DateAcquisition { get; set; }
        //public DateTime? SaleDate {  get; set; }
    }
}
