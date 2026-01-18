using OrderReadService.Application.Ports;
using StackExchange.Redis;

using System.Text.Json;

namespace OrderReadService.Infrastructure.Cache
{
    public class RedisCacheService : ICacheService
    {

        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);

            if (!value.HasValue)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, ttl);
        }
    }
}
