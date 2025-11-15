using Common.Core.Configuration;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Jobs
{
    public static class HangfireRegistrar
    {
        public static void Register(IServiceCollection services, IConfiguration config)
        {
            // Fix: Add missing using for Hangfire.MemoryStorage and call AddMemoryStorage instead of UseMemoryStorage
            services.AddHangfire(x => x.UseMemoryStorage()); // Ensure Hangfire.MemoryStorage is referenced

            services.AddHangfireServer();

            // Optional: register a background job service
            services.AddTransient<IBackgroundJobService, BackgroundJobService>();
        }
    }
}
