using System.ComponentModel;

namespace gestao_de_portfolio.Enums
{
    public enum UserTypeEnum
    {
        [Description("administrador")]
        admin = 1,
        [Description("cliente")]
        client = 2
    }
}
