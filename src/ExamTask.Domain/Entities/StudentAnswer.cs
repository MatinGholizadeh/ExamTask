namespace ExamTask.Domain.Entities;

public class StudentAnswer
{
    public long Id { get; set; }

    public long StudentId { get; set; }
    public Student Student { get; set; } = default!;

    public long QuestionId { get; set; }
    public Question Question { get; set; } = default!;

    public int? FirstAnswerOption { get; set; } // Option 1..4 or null
    public DateTime FirstAnsweredAt { get; set; }

    public int? LastAnswerOption { get; set; } // Even if cleared, we save the new state (null if cleared)
    public DateTime? LastAnsweredAt { get; set; } // null if never changed after first answer

    public bool IsFinalized { get; set; } // True after student clicks Submit. No more edits allowed.
}
