namespace ExamTask.Domain.Entities;

public class StudentAnswer
{
    public long Id { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; } = default!;

    public long StudentExamId { get; set; }
    public StudentExam StudentExam { get; set; } = default!;

    public int? FirstAnswerOption { get; set; } // If it doesn't respond, it will remain null
    public DateTime? FirstAnsweredAt { get; set; }

    public int? LastAnswerOption { get; set; } // Even if it is null (the response has been deleted).
    public DateTime? LastAnsweredAt { get; set; }

    public int ChangeCount { get; set; } = 0; // Number of times the answer was edited.

    // Calculated: Was it allowed outside of time?
    public bool IsOutsideAllowedTime { get; set; }
}
