namespace Common.Core.Configuration
{
    public class ModuleSettings
    {
        public string Caching { get; set; } = "InMemory"; // Redis or InMemory
        public bool BackgroundJobs { get; set; } = false;
        public string Reporting { get; set; } = "None"; // Pdf, Excel, All, None

        // Optional modules
        public bool EnableEmail { get; set; } = true;
        public bool EnableSms { get; set; } = true;
        public bool EnableAuth { get; set; } = true;
        public bool EnableHttp { get; set; } = true;
        public bool EnableAudit { get; set; } = true;
        public bool EnableAutoMapper { get; set; } = true;
        public bool EnableFileStorage { get; set; } = true;
        public bool EnableQueue { get; set; } = true;

        // Middleware flags
        public bool EnableProblemDetails { get; set; } = true;
        public bool EnableRequestResponseLogging { get; set; } = true;
        public bool EnableCorrelationId { get; set; } = true;
        public bool EnableSwagger { get; set; } = true; // can enable outside dev
    }
}
