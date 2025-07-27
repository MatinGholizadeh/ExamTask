using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Application.Common.Exceptions;
using ExamTask.Domain.Entities;
using MediatR;

namespace ExamTask.Application.Features.Exams.Commands.SubmitAnswer;

public class SubmitAnswerCommandHandler : IRequestHandler<SubmitAnswerCommand, Unit>
{
    private readonly IStudentAnswerRepository _studentAnswerRepository;
    private readonly IStudentExamRepository _studentExamRepository;

    public SubmitAnswerCommandHandler(IStudentAnswerRepository studentAnswerRepository, IStudentExamRepository studentExamRepository)
    {
        _studentAnswerRepository = studentAnswerRepository;
        _studentExamRepository = studentExamRepository;
    }

    public async Task<Unit> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
    {
        var studentExam = await _studentExamRepository.GetByIdAsync(request.StudentExamId, cancellationToken);

        if (studentExam == null)
            throw new NotFoundException("Student exam not found.");

        var studentAnswer = await _studentAnswerRepository.GetByQuestionAndStudentExamAsync(request.QuestionId, request.StudentExamId, cancellationToken);

        if (studentAnswer == null)
        {
            studentAnswer = new StudentAnswer
            {
                StudentExamId = request.StudentExamId,
                QuestionId = request.QuestionId,
                FirstAnswerOption = request.AnswerOption,
                FirstAnsweredAt = request.AnsweredAt,
                ChangeCount = 0, // Initial answer count
                IsOutsideAllowedTime = false, // Assuming it’s within the allowed time
            };

            await _studentAnswerRepository.AddAsync(studentAnswer, cancellationToken);
        }
        else
        {
            studentAnswer.LastAnswerOption = request.AnswerOption;
            studentAnswer.LastAnsweredAt = request.AnsweredAt;
            studentAnswer.ChangeCount++;

            // Optionally, check if the answer is outside the allowed time and update IsOutsideAllowedTime

            await _studentAnswerRepository.UpdateAsync(studentAnswer, cancellationToken);
        }

        return Unit.Value;
    }
}