using ExamTask.Domain.Entities;

namespace ExamTask.Application.Abstractions.Persistence.Repositories;

public interface IQuestionRepository
{
    // Query
    Task<List<Question>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int questionId, CancellationToken cancellationToken = default);

    // Command
    Task AddAsync(Question question, CancellationToken cancellationToken = default);
    Task UpdateAsync(Question question, CancellationToken cancellationToken = default);
    Task DeleteAsync(Question question, CancellationToken cancellationToken = default);
}
