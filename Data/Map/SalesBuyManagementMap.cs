using gestao_de_portfolio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao_de_portfolio.Data.Map
{
    public class SalesBuyManagementMap : IEntityTypeConfiguration<SalesBuyManagementModel>
    {
        public void Configure(EntityTypeBuilder<SalesBuyManagementModel> builder)
        {
            builder.HasKey(x => x.Id);
            // Configuração da chave estrangeira
            builder.HasIndex(x => x.UserId).IsUnique(false);
            builder.HasOne<UsersModel>()
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.ProductId).IsUnique(false);
            builder.HasOne<ProductsModel>()
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.BuyDate);
            builder.Property(x => x.SaleDate);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
