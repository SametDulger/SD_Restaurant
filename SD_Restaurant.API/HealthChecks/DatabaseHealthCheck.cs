using Microsoft.Extensions.Diagnostics.HealthChecks;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.API.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly RestaurantDbContext _context;

        public DatabaseHealthCheck(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Database.CanConnectAsync(cancellationToken);
                return HealthCheckResult.Healthy("Database is accessible");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is not accessible", ex);
            }
        }
    }
} 