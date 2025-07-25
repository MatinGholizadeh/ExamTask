namespace ExamTask.Domain.Entities;

public class Exam
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationMinutes { get; set; } // Time limit.

    // Reference Navigation Properties - ExamGroup
    public long ExamGroupId { get; set; }
    public ExamGroup Group { get; set; } = default!;

    public ICollection<Question> Questions { get; set; } = default!;
}
