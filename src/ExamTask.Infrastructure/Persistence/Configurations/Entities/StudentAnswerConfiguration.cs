using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Entities;

public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstAnswerOption);
        builder.Property(x => x.FirstAnsweredAt);
        builder.Property(x => x.LastAnswerOption);
        builder.Property(x => x.LastAnsweredAt);
        builder.Property(x => x.ChangeCount).IsRequired();
        builder.Property(x => x.IsOutsideAllowedTime).IsRequired();

        builder.HasOne(x => x.StudentExam)
            .WithMany(e => e.Answers)
            .HasForeignKey(x => x.StudentExamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
