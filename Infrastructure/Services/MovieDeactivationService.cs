using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;

namespace Infrastructure.Services;

public class MovieDeactivationService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DeactivateExpiredMoviesAsync();

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task DeactivateExpiredMoviesAsync()
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var moviesToDeactivate = await context.Movies
            .Include(m => m.Showtimes)
            .Where(m => m.IsActive && !m.Showtimes.Any(s => s.StartTime > DateTime.UtcNow))
            .ToListAsync();
                  
        if (moviesToDeactivate.Count != 0)
        {
            foreach (var movie in moviesToDeactivate)
            {
                movie.IsActive = false;

                movie.Showtimes.Clear();
            }
        }

        await context.SaveChangesAsync();
    }
}
