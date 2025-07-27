using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Domain.Entities;

namespace ExamTask.Application.Features.StudentAnswers.Services;

public class StudentAnswerService
{
    private readonly IExamRepository _examRepository;
    private readonly IStudentExamRepository _studentExamRepository;

    public StudentAnswerService(
        IExamRepository examRepository,
        IStudentExamRepository studentExamRepository)
    {
        _examRepository = examRepository;
        _studentExamRepository = studentExamRepository;
    }

    public async Task UpdateAnswerAsync(
        StudentAnswer answer,
        int? selectedOption,
        DateTime answeredAt,
        CancellationToken cancellationToken = default)
    {
        // Load related data (Exam, StudentExam)
        var studentExam = await _studentExamRepository.GetByIdAsync(answer.StudentExamId, cancellationToken)
                            ?? throw new Exception("StudentExam not found");

        var exam = await _examRepository.GetByIdAsync(studentExam.ExamId, cancellationToken)
                    ?? throw new Exception("Exam not found");

        // First answer
        if (answer.FirstAnswerOption is null)
        {
            answer.FirstAnswerOption = selectedOption;
            answer.FirstAnsweredAt = answeredAt;
        }

        // Update last answer and stats
        answer.LastAnswerOption = selectedOption;
        answer.LastAnsweredAt = answeredAt;
        answer.ChangeCount++;

        // Deadline = StartedAt + DurationMinutes
        var deadline = studentExam.StartedAt.AddMinutes(exam.DurationMinutes);
        answer.IsOutsideAllowedTime = answeredAt > deadline;
    }
}