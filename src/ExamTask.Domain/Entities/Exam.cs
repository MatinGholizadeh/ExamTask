namespace ExamTask.Domain.Entities;

public class Exam
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } //Exam access start date.
    public DateTime EndDate { get; set; } // Access end date.

    public int DurationMinutes { get; set; } // Time allowed after the test starts (e.g. 90 minutes)

    // Reference Navigation Properties - ExamGroup
    public long ExamGroupId { get; set; }
    public ExamGroup ExamGroup { get; set; } = default!;

    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>(); // newly Added!!!!
}
