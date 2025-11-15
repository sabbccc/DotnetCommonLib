using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.SqlServer; // For UseSqlServer
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // For UseMySql

namespace Common.Core.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Look for appsettings.json in the startup project folder (adjust path if needed)
        var basePath = Directory.GetCurrentDirectory();
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var dbSection = config.GetSection("Database");
        var provider = (dbSection.GetValue<string>("Provider") ?? "SqlServer").Trim();
        var connection = dbSection.GetValue<string>("ConnectionString")
                         ?? throw new InvalidOperationException("ConnectionString is required for migrations.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        switch (provider.ToLower())
        {
            case "sqlserver":
                optionsBuilder.UseSqlServer(connection);
                break;
            case "mysql":
                optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
                break;
            case "postgresql":
                // Uncomment when ready
                 optionsBuilder.UseNpgsql(connection);
                break;
            case "inmemory":
                var name = dbSection.GetValue<string>("InMemoryDatabaseName") ?? "DesignTimeInMemory";
                optionsBuilder.UseInMemoryDatabase(name);
                break;
            default:
                throw new InvalidOperationException($"Unsupported provider for design time: {provider}");
        }

        return new AppDbContext(optionsBuilder.Options);
    }
}
