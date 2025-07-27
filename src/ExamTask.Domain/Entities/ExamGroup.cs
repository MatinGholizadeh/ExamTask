namespace ExamTask.Domain.Entities;

public class ExamGroup
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
