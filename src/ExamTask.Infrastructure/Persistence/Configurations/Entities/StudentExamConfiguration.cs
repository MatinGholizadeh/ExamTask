using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Entities;

public class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam>
{
    public void Configure(EntityTypeBuilder<StudentExam> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartedAt).IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Exam)
            .WithMany(e => e.StudentExams)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}