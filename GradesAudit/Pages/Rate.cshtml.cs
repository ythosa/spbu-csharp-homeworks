using GradesAudit.Data;
using GradesAudit.Models;

namespace GradesAudit.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[BindProperties]
public class Rate : PageModel
{
    public AuditRecord AuditRecord { get; set; } = new();

    private readonly GradesAuditDbContext _context;
    public Rate(GradesAuditDbContext context)
        => _context = context;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.AuditRecords.Add(AuditRecord);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
