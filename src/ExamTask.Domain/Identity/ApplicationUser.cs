using Microsoft.AspNetCore.Identity;

namespace ExamTask.Domain.Identity;

/// <summary>
/// Custom identity user with role-based properties.
/// </summary>
public class ApplicationUser : IdentityUser<long>
{
    public string FullName { get; set; } = default!;
    public string NationalCode { get; set; } = default!;
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
