using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Storage
{
    public static class FileStorageExtensions
    {
        public static IServiceCollection AddLocalFileStorage(this IServiceCollection services, string basePath)
        {
            services.AddSingleton<IFileStorageService>(sp => new LocalFileStorageService(basePath));
            return services;
        }
    }
}
