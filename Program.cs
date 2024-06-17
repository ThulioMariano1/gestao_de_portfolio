
using gestao_de_portfolio.Data;
using gestao_de_portfolio.Services;
using gestao_de_portfolio.Services.Interfaces;
using gestao_de_portfolio.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace gestao_de_portfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public IConfigurationRoot Configuration;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<WebApplicationProgram>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("DataBase");
                services.AddDbContext<PortfolioDBContext>(options => options.UseSqlServer(connectionString));
                services.AddScoped<IProcessOrdersService, ProcessOrdersService>();
                services.AddHostedService<ProcessWorker>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
    }
}
