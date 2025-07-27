using ExamTask.Domain.Entities;
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

    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }
    public DbSet<StudentExam> StudentExams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}