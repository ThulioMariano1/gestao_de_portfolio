using gestao_de_portfolio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao_de_portfolio.Data.Map
{
    public class ProductsMap : IEntityTypeConfiguration<ProductsModel>
    {
        public void Configure(EntityTypeBuilder<ProductsModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.OtherDetails).IsRequired().HasMaxLength(1000);
        }
    }
}
