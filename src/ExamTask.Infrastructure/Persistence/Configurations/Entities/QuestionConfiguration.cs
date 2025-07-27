using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Entities;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).IsRequired();
        builder.Property(x => x.Option1).IsRequired();
        builder.Property(x => x.Option2).IsRequired();
        builder.Property(x => x.Option3).IsRequired();
        builder.Property(x => x.Option4).IsRequired();

        builder.Property(x => x.CorrectOption).IsRequired();

        builder.HasOne(x => x.Exam)
            .WithMany(e => e.Questions)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}