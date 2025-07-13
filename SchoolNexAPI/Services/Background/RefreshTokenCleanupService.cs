using SchoolNexAPI.Repositories.Abstract;

namespace SchoolNexAPI.Services.Background
{
    public class RefreshTokenCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RefreshTokenCleanupService> _logger;

        public RefreshTokenCleanupService(IServiceScopeFactory scopeFactory, ILogger<RefreshTokenCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

                try
                {
                    await repo.CleanupExpiredAsync();
                    _logger.LogInformation("Refresh token cleanup executed at {Time}", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during refresh token cleanup");
                }

                // Wait before next cleanup run (every 6 hours)
                await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
            }
        }
    }
}
