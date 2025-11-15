using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Audit
{
    public static class AuditExtensions
    {
        public static IServiceCollection AddAuditLogging(this IServiceCollection services)
        {
            services.AddScoped<IAuditService, EfAuditService>();
            return services;
        }
    }
}
