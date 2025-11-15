using Common.Core.Configuration;
using Common.Core.Jobs;
using Common.Core.Services.Cache;
using Common.Core.Services.Reporting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Core.Services
{
    public static class ModuleRegistrar
    {
        public static void RegisterModules(IServiceCollection services, IConfiguration config)
        {
            var modules = config.GetSection("Modules").Get<ModuleSettings>() ?? new ModuleSettings();

            // --- CACHING ---
            if (modules.Caching == "Redis")
            {
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheService, InMemoryCacheService>();
            }

            // --- REPORTING ---
            switch (modules.Reporting)
            {
                case "Pdf":
                    services.AddTransient<IPdfService, PdfService>();
                    break;
                case "Excel":
                    services.AddTransient<IExcelService, ExcelService>();
                    break;
                case "All":
                    services.AddTransient<IPdfService, PdfService>();
                    services.AddTransient<IExcelService, ExcelService>();
                    break;
                case "None":
                    break;
                default:
                    throw new InvalidOperationException($"Invalid reporting module: {modules.Reporting}");
            }

            // --- BACKGROUND JOBS ---
            if (modules.BackgroundJobs)
            {
                HangfireRegistrar.Register(services, config);
            }
        }
    }
}
