using GradesAudit.Data;
using GradesAudit.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GradesAudit.Pages;

public class Audit : PageModel
{
    private readonly GradesAuditDbContext context;


    public Audit(GradesAuditDbContext context) => this.context = context;

    public IList<AuditRecord> AuditRecords { get; private set; } = new List<AuditRecord>();

    public void OnGet()
    {
        AuditRecords = context.AuditRecords.OrderBy(auditRecord => auditRecord.Id).ToList();
    }
}
