using ExamTask.Domain.Identity;

namespace ExamTask.Domain.Entities;

/// <summary>
/// Represents a student's individual session for taking an exam.
/// Each student may start the exam at a different time within the allowed window (StartDate to EndDate),
/// and their allowed duration is calculated from the exact time they begin (StartedAt) plus the exam's DurationMinutes.
/// This entity helps track the personalized exam timing and submission status for each student.
/// </summary>
public class StudentExam
{
    public long Id { get; set; }

    public long StudentId { get; set; }
    public ApplicationUser Student { get; set; } = default!;

    public long ExamId { get; set; }
    public Exam Exam { get; set; } = default!;

    public DateTime StartedAt { get; set; } // When the exam is started.
    public DateTime? SubmittedAt { get; set; } // When the "Submit" button is pressed.

    public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();
}
