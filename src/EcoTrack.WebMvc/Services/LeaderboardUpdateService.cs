using System;
using System.Threading;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EcoTrack.WebMvc.Services
{
    public class LeaderboardUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LeaderboardUpdateService> _logger;

        public LeaderboardUpdateService(IServiceProvider serviceProvider, ILogger<LeaderboardUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run the calculation every hour
            using var timer = new PeriodicTimer(TimeSpan.FromHours(1));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    _logger.LogInformation("Leaderboard update job is running.");

                    // We must create a "scope" to get our services in a background task
                    await using var scope = _serviceProvider.CreateAsyncScope();
                    var leaderboardService = scope.ServiceProvider.GetRequiredService<ILeaderboardService>();

                    // Call the update method for both periods
                    await leaderboardService.UpdateLeaderboardAsync("Weekly");
                    await leaderboardService.UpdateLeaderboardAsync("Monthly");

                    _logger.LogInformation("Leaderboard update job finished successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the leaderboard.");
                }
            }
        }
    }
}