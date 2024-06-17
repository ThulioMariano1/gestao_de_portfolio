using gestao_de_portfolio.DTO.Request;
using gestao_de_portfolio.Enums;

namespace gestao_de_portfolio.Models
{
    public class UsersModel
    {
        public UsersModel(CreateUserDTO user) {
            Name = user.Name;
            Email = user.Email;
            PassWord = user.PassWord;
            Type = user.Type;
            Money = user.Money;
        }
        public UsersModel() { }
        public int Id { get ; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; } // (criptografada)
        public UserTypeEnum? Type { get; set; }// (administrador, cliente)
        public double? Money { get; set; }
    }
}
