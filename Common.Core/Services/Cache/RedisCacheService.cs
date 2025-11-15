using StackExchange.Redis;
using System.Text.Json;

namespace Common.Core.Services.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public T? Get<T>(string key)
        {
            var val = _db.StringGet(key);
            return val.HasValue ? JsonSerializer.Deserialize<T>(val!) : default;
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            var data = JsonSerializer.Serialize(value);
            _db.StringSet(key, data, expiry ?? TimeSpan.FromMinutes(60));
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }
    }
}
