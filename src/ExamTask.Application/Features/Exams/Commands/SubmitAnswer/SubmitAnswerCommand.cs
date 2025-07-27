using MediatR;

namespace ExamTask.Application.Features.Exams.Commands.SubmitAnswer;

public class SubmitAnswerCommand : IRequest<Unit>
{
    public long StudentExamId { get; set; }
    public long QuestionId { get; set; }
    public int AnswerOption { get; set; }
    public DateTime AnsweredAt { get; set; }
}
