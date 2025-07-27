using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Application.Common.Exceptions;
using ExamTask.Domain.Entities;
using MediatR;

namespace ExamTask.Application.Features.Exams.Commands.StartExam;

public class StartExamCommandHandler : IRequestHandler<StartExamCommand, long>
{
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly IExamRepository _examRepository;

    public StartExamCommandHandler(IStudentExamRepository studentExamRepository, IExamRepository examRepository)
    {
        _studentExamRepository = studentExamRepository;
        _examRepository = examRepository;
    }

    public async Task<long> Handle(StartExamCommand request, CancellationToken cancellationToken)
    {
        var exam = await _examRepository.GetByIdAsync(request.ExamId, cancellationToken);

        if (exam == null)
            throw new NotFoundException("Exam not found.");

        var studentExam = new StudentExam
        {
            StudentId = request.StudentId,
            ExamId = request.ExamId,
            StartedAt = request.StartDate,
            SubmittedAt = null, // Not submitted yet
        };

        await _studentExamRepository.AddAsync(studentExam, cancellationToken);
        return studentExam.Id;
    }
}