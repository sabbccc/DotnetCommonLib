using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common.Core.Configuration;

namespace Common.Core.Database;

public static class DatabaseBootstrapper
{
    public static IServiceCollection AddDatabaseModule(
        this IServiceCollection services,
        DatabaseSettings settings)
    {
        if (settings is null)
            throw new ArgumentNullException(nameof(settings));

        services.AddDbContext<AppDbContext>(options =>
        {
            var provider = (settings.Provider ?? "SqlServer").Trim();

            switch (provider.ToLower())
            {
                case "sqlserver":
                    options.UseSqlServer(settings.ConnectionString);
                    break;
                case "mysql":
                    options.UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));
                    break;
                case "postgresql":
                     options.UseNpgsql(settings.ConnectionString);
                    break;
                case "inmemory":
                    options.UseInMemoryDatabase(settings.InMemoryDatabaseName ?? "InMemoryDb");
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported database provider: {settings.Provider}");
            }
        });

        // Optional: register a factory for getting DbContext when needed
        services.AddScoped<Func<AppDbContext>>(sp => () => sp.GetRequiredService<AppDbContext>());

        return services;
    }
}
