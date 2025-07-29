using Microsoft.Extensions.Caching.Distributed;
using SD_Restaurant.Application.Services;
using System.Text.Json;

namespace SD_Restaurant.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value, _jsonOptions);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var jsonValue = JsonSerializer.Serialize(value, _jsonOptions);
            var options = new DistributedCacheEntryOptions();

            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }

            await _distributedCache.SetStringAsync(key, jsonValue, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            // Redis doesn't support pattern removal directly in .NET
            // This would require Redis SCAN command implementation
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return !string.IsNullOrEmpty(value);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            var cachedValue = await GetAsync<T>(key);
            if (cachedValue != null)
                return cachedValue;

            var value = await factory();
            await SetAsync(key, value, expiration);
            return value;
        }
    }
} 