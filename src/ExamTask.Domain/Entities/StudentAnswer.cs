namespace ExamTask.Domain.Entities;

public class StudentAnswer
{
    public long Id { get; set; }

    // Reference Navigation Properties - Student
    public long StudentId { get; set; }
    public Student Student { get; set; } = default!;

    // Reference Navigation Properties - Question
    public long QuestionId { get; set; }
    public Question Question { get; set; } = default!;

    public int? SelectedOption { get; set; } // null => If the student did not want to answer.

    public DateTime AnsweredAt { get; set; } // For comparison with the test time
}
