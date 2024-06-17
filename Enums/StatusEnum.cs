using System.ComponentModel;

namespace gestao_de_portfolio.Enums
{
    public enum StatusEnum
    {
        [Description("Compra Registrada na bolsa")]
        CompraRegistrada = 1,
        [Description("Compra executada em partes")]
        CompraExecutadaEmPartes = 2,
        [Description("Compra finalizada")]
        CompraFinalizada = 3,
        [Description("Compra Cancelada")]
        CompraCancelada = 4,

        [Description("Venda registrada na bolsa")]
        VendaRegistrada = 5,
        [Description("Venda executada em partes")]
        VendaExecutadaEmPartes = 6,
        [Description("Venda Finalizada")]
        VendaFinalizada = 7,
        [Description("Venda Cancelada")]
        VendaCancelada = 8
    }
}
