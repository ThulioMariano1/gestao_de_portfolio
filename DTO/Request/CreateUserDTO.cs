using gestao_de_portfolio.Enums;

namespace gestao_de_portfolio.DTO.Request
{
    public class CreateUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        public UserTypeEnum? Type { get; set; }// (administrador, cliente)
        public double? Money { get; set; }
    }
}
