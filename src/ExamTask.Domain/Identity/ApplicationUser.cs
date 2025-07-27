using ExamTask.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExamTask.Domain.Identity;

/// <summary>
/// Custom identity user with role-based properties.
/// </summary>
public class ApplicationUser : IdentityUser<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = default!;
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
}
