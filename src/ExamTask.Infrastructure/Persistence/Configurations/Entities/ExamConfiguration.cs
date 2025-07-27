using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Entities;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.DurationMinutes).IsRequired();

        builder.HasOne(x => x.ExamGroup)
            .WithMany(g => g.Exams)
            .HasForeignKey(x => x.ExamGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}