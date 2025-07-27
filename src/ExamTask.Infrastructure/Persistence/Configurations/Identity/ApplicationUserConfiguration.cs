using ExamTask.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Identity;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.NationalCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(u => u.ProfileImageUrl)
            .HasMaxLength(500);

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        // رابطه با StudentExams
        builder.HasMany(u => u.StudentExams)
            .WithOne(se => se.Student)
            .HasForeignKey(se => se.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}