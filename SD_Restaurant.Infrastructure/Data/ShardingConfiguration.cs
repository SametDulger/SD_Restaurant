using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SD_Restaurant.Infrastructure.Data
{
    public class ShardingConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _shardConnections;

        public ShardingConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _shardConnections = new Dictionary<string, string>
            {
                { "shard1", _configuration.GetConnectionString("Shard1") ?? "" },
                { "shard2", _configuration.GetConnectionString("Shard2") ?? "" },
                { "shard3", _configuration.GetConnectionString("Shard3") ?? "" }
            };
        }

        public string GetShardConnectionString(int entityId)
        {
            // Simple hash-based sharding
            var shardIndex = entityId % _shardConnections.Count;
            var shardKey = $"shard{shardIndex + 1}";
            
            return _shardConnections[shardKey];
        }

        public DbContextOptions<RestaurantDbContext> GetShardDbContextOptions(int entityId)
        {
            var connectionString = GetShardConnectionString(entityId);
            
            return new DbContextOptionsBuilder<RestaurantDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public List<string> GetAllShardConnectionStrings()
        {
            return _shardConnections.Values.ToList();
        }

        public async Task<bool> ValidateShardConnectionsAsync()
        {
            foreach (var shard in _shardConnections)
            {
                try
                {
                    using var context = new RestaurantDbContext(
                        new DbContextOptionsBuilder<RestaurantDbContext>()
                            .UseSqlServer(shard.Value)
                            .Options);
                    
                    await context.Database.CanConnectAsync();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
} 