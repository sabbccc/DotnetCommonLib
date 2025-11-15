using System.Threading.Tasks;

namespace Common.Core.Audit
{
    public interface IAuditService
    {
        Task LogAsync(AuditLog log);
    }
}
