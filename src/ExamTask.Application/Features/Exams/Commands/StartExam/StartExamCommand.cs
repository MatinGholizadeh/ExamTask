using MediatR;

namespace ExamTask.Application.Features.Exams.Commands.StartExam;

public class StartExamCommand : IRequest<long>  // Returns the StudentExamId after creating it
{
    public long StudentId { get; set; }
    public long ExamId { get; set; }
    public DateTime StartDate { get; set; }
}