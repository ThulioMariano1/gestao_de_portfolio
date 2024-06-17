using System.Net.Mail;
using System.Net;
using gestao_de_portfolio.Data;
using Microsoft.EntityFrameworkCore;
using gestao_de_portfolio.DTO.Response;
using gestao_de_portfolio.Services.Interfaces;

namespace gestao_de_portfolio.Services
{
    public class EmailService: IEmailService
    {
        private string _smtpServer = "smtp.gmail.com";
        private int _smtpPort = 587;
        private string _smtpUsername = "thuliomariano2@gmail.com";
        private string _smtpPassword = "frro nfoe pjcw lrnv";
        private readonly PortfolioDBContext _dbContext;

        public EmailService( PortfolioDBContext portfolioDBContext)
        {
            _dbContext = portfolioDBContext;
        }

        public async Task SendEmail()
        {
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(_smtpUsername);
                message.To.Add(new MailAddress("thuliomariano1@gmail.com"));
                message.Subject = "Relação de Produtos";
                message.Body = "Segue a relação de produtos: \n\n\n";
                message.IsBodyHtml = true; // Defina como true se o corpo do email for HTML

                using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    client.EnableSsl = true;
                    var result = await GetOrders();
                    message.Body += result;
                    client.Send(message);
                }
            }
        }

        private async Task<List<EmailResponseDTO>> GetOrders()
        {
            return await _dbContext.SalesBuyManagement
                .Join(_dbContext.Products, sell => sell.ProductId,
                      p => p.Id,
                      (sell, pr) => new { Sales = sell, Products = pr })
                .GroupBy(g => new { g.Sales.UserId, g.Sales.ProductId, g.Sales.Status, g.Products.Name, g.Products.Price })
                .Select(g => new EmailResponseDTO
                {
                    UserId = g.Key.UserId,
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    ProductPrice = g.Key.Price,
                    Price = g.Sum(p => p.Sales.Price * p.Sales.Amount) / g.Sum(p => p.Sales.Amount),
                    Amount = g.Sum(p => p.Sales.Amount)
                })
                .OrderBy(o => o.UserId)
                .ThenBy(t => t.ProductId)
                .ToListAsync();
        }
    }
}
