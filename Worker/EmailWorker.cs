
using gestao_de_portfolio.Services.Interfaces;

namespace gestao_de_portfolio.Worker
{
    public class EmailWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public EmailWorker(IServiceScopeFactory serviceScopeFactory )
        {
            _scopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var email = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    await email.SendEmail();
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
