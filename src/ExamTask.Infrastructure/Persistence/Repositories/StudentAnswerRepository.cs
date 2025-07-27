using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Domain.Entities;
using ExamTask.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.Infrastructure.Persistence.Repositories;

public class StudentAnswerRepository : IStudentAnswerRepository
{
    private readonly IdentityDbContext _context;

    public StudentAnswerRepository(IdentityDbContext context)
    {
        _context = context;
    }

    #region ExistAsync

    public async Task<bool> ExistsAsync(int studentAnswerId, CancellationToken cancellationToken = default)
    {
        return await _context.StudentAnswers
            .AnyAsync(sa => sa.Id == studentAnswerId, cancellationToken);
    }

    #endregion ExistAsync - End

    #region GetAllAsync

    public async Task<List<StudentAnswer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.StudentAnswers.ToListAsync(cancellationToken);
    }

    #endregion GetAllAsync - End

    #region GetByIdAsync

    public async Task<StudentAnswer?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.StudentAnswers
            .Include(sa => sa.Question)
            .Include(sa => sa.StudentExam)
            .FirstOrDefaultAsync(sa => sa.Id == id, cancellationToken);
    }

    #endregion GetByIdAsync - End

    #region GetByQuestionAndStudentExamAsync

    public async Task<StudentAnswer?> GetByQuestionAndStudentExamAsync(long questionId, long studentExamId, CancellationToken cancellationToken)
    {
        return await _context.StudentAnswers.FirstOrDefaultAsync(sa => 
            sa.QuestionId == questionId && sa.StudentExamId == studentExamId, cancellationToken);
    }

    #endregion GetByQuestionAndStudentExamAsync - End

    #region AddAsync

    public async Task AddAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default)
    {
        _context.StudentAnswers.Add(studentAnswer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion AddAsync - End

    #region DeleteAsync

    public async Task DeleteAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default)
    {
        _context?.StudentAnswers.Remove(studentAnswer);
        await _context?.SaveChangesAsync(cancellationToken);
    }

    #endregion DeleteAsync - End

    #region UpdateAsync

    public async Task UpdateAsync(StudentAnswer studentAnswer, CancellationToken cancellationToken = default)
    {
        _context.StudentAnswers.Update(studentAnswer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion UpdateAsync - End
}
