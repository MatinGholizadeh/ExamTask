using ExamTask.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.Infrastructure.Identity;

/// <summary>
/// Identity context for ApplicationUser and roles.
/// </summary>
public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }
}