namespace ExamTask.Domain.Entities;

public class Question
{
    public long Id { get; set; }

    public string Text { get; set; } = string.Empty;

    // Options - Fixed 4 choices
    public string Option1 { get; set; } = string.Empty;
    public string Option2 { get; set; } = string.Empty;
    public string Option3 { get; set; } = string.Empty;
    public string Option4 { get; set; } = string.Empty;

    // Stores the correct option index (1 to 4)
    public int CorrectOption { get; set; }

    // Reference Navigation Property - Exam
    public long ExamId { get; set; }
    public Exam Exam { get; set; } = default!;
}
