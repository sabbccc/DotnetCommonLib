using System.Threading.Tasks;
using Common.Core.Database;

namespace Common.Core.Audit
{
    public class EfAuditService : IAuditService
    {
        private readonly AppDbContext _db;

        public EfAuditService(AppDbContext db)
        {
            _db = db;
        }

        public async Task LogAsync(AuditLog log)
        {
            _db.Set<AuditLog>().Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
