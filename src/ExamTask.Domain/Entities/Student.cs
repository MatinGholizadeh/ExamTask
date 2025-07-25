using Microsoft.AspNetCore.Identity;

namespace ExamTask.Domain.Entities;

public class Student : IdentityUser<long>
{
    public string FullName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }

    public ICollection<StudentAnswer> Answers { get; set; } = default!;
}
