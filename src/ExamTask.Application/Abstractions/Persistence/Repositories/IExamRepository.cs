using ExamTask.Domain.Entities;

namespace ExamTask.Application.Abstractions.Persistence.Repositories;

public interface IExamRepository
{
    // Query
    Task<List<Exam>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Exam?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int examId, CancellationToken cancellationToken = default);

    // Command
    Task AddAsync(Exam exam, CancellationToken cancellationToken = default);
    Task UpdateAsync(Exam exam, CancellationToken cancellationToken = default);
    Task DeleteAsync(Exam exam, CancellationToken cancellationToken = default);
}
