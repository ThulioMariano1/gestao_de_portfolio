
using gestao_de_portfolio.Services.Interfaces;

namespace gestao_de_portfolio.Worker
{
    public class ProcessWorker : BackgroundService
    {
        private readonly ILogger<ProcessWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public ProcessWorker(ILogger<ProcessWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                   var processOrdersService = scope.ServiceProvider.GetRequiredService<IProcessOrdersService>();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await processOrdersService.ProcessOrdersAsync();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
