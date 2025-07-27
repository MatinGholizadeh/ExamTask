using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamTask.Infrastructure.Persistence.Configurations.Entities;

public class ExamGroupConfiguration : IEntityTypeConfiguration<ExamGroup>
{
    public void Configure(EntityTypeBuilder<ExamGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}