using ExamTask.Domain.Entities;

namespace ExamTask.Application.Abstractions.Persistence.Repositories;

public interface IStudentExamRepository
{
    // Query
    Task<List<StudentExam>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StudentExam?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int studentExamId, CancellationToken cancellationToken = default);

    // Command
    Task AddAsync(StudentExam studentExam, CancellationToken cancellationToken = default);
    Task UpdateAsync(StudentExam studentExam, CancellationToken cancellationToken = default);
    Task DeleteAsync(StudentExam studentExam, CancellationToken cancellationToken = default);
}
