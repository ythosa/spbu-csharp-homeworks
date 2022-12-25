using GradesAudit.Models;
using Microsoft.EntityFrameworkCore;

namespace GradesAudit.Data;

public class GradesAuditDbContext : DbContext
{
    public GradesAuditDbContext(DbContextOptions<GradesAuditDbContext> options) : base(options)
    {
    }

    public DbSet<AuditRecord> AuditRecords => Set<AuditRecord>();
}
