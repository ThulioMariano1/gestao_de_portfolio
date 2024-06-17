using gestao_de_portfolio.Data.Map;
using gestao_de_portfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace gestao_de_portfolio.Data
{
    public class PortfolioDBContext:DbContext
    {

        public PortfolioDBContext( DbContextOptions<PortfolioDBContext> options): base(options)
        {
        }

        public DbSet<ProductsModel> Products { get; set; }
        public DbSet<InvestmentsModel> Investments { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<SalesBuyManagementModel> SalesBuyManagement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new ProductsMap());
            modelBuilder.ApplyConfiguration(new InvestmentMap());
            modelBuilder.ApplyConfiguration(new SalesBuyManagementMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
