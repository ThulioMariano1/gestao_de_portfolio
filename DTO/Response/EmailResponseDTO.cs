namespace gestao_de_portfolio.DTO.Response
{
    public class EmailResponseDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double? Price { get; set; }
        public int? Amount { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
    }
}
