using System.ComponentModel.DataAnnotations;

namespace GradesAudit.Models;

public class AuditRecord
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter student name")]
    [StringLength(30, ErrorMessage = "Length of student name must by lower than 30")]
    public string StudentName { get; set; } = "";

    [Required(ErrorMessage = "Please enter subject")]
    [StringLength(15, ErrorMessage = "Subject length must by lower than 15")]
    public string Subject { get; set; } = "";

    [Required(ErrorMessage = "Please enter assessment")]
    [Range(2, 5, ErrorMessage = "Assessment must be from 2 to 5")]
    public uint Assessment { get; set; }
}
