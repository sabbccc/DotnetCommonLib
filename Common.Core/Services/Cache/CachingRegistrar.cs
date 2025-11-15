using Common.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Common.Core.Services.Cache
{
    public static class CachingRegistrar
    {
        public static void Register(IServiceCollection services, IConfiguration config)
        {
            var modules = config.GetSection("Modules").Get<ModuleSettings>() ?? new ModuleSettings();

            if (modules.Caching == "Redis")
            {
                // Ensure Redis connection string exists in config
                var redisConnection = config.GetValue<string>("Redis:ConnectionString");
                if (string.IsNullOrEmpty(redisConnection))
                    throw new InvalidOperationException("Redis connection string is missing in appsettings.json");

                var multiplexer = ConnectionMultiplexer.Connect(redisConnection);
                services.AddSingleton<IConnectionMultiplexer>(multiplexer);
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheService, InMemoryCacheService>();
            }
        }
    }
}
