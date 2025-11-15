using System;

namespace Common.Core.Audit
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string? UserId { get; set; }          // Optional: logged-in user
        public string Action { get; set; } = null!;
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
