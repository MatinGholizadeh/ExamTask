using ExamTask.Domain.Entities;

namespace ExamTask.Application.Abstractions.Persistence.Repositories;

public interface IStudentAnswerRepository
{
    // Query
    Task<List<StudentAnswer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StudentAnswer?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int studentAnswerId, CancellationToken cancellationToken = default);
    Task<StudentAnswer?> GetByQuestionAndStudentExamAsync(long questionId, long studentExamId, CancellationToken cancellationToken);

    // Command
    Task AddAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default);
    Task UpdateAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default);
    Task DeleteAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default);
}
