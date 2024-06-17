namespace gestao_de_portfolio.Models
{
    public class ProductsModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public double? Price { get; set; }
        public string? OtherDetails { get; set; }
    }
}
