using gestao_de_portfolio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gestao_de_portfolio.Data.Map
{
    public class UsersMap : IEntityTypeConfiguration<UsersModel>
    {
        public void Configure(EntityTypeBuilder<UsersModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PassWord).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Money).IsRequired();
        }
    }
}
