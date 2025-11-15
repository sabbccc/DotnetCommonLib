namespace Common.Core.Configuration;

public class DatabaseSettings
{
    public string Provider { get; set; } = "SqlServer";
    public string ConnectionString { get; set; } = "";
    // Optional: name for in-memory database
    public string? InMemoryDatabaseName { get; set; }
}
