using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Security
{
    public static class CryptoServiceExtensions
    {
        public static IServiceCollection AddCryptoService(this IServiceCollection services)
        {
            services.AddSingleton<ICryptoService, CryptoService>();
            return services;
        }
    }
}
