using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Domain.Entities;
using ExamTask.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.Infrastructure.Persistence.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly IdentityDbContext _context;

    public QuestionRepository(IdentityDbContext context)
    {
        _context = context;
    }

    #region ExistsAsync

    public async Task<bool> ExistsAsync(int questionId,CancellationToken cancellationToken = default)
    {
        return await _context.Questions
            .AnyAsync(q => q.Id == questionId, cancellationToken);
    }

    #endregion ExistsAsync - End

    #region GetAllAsync

    public async Task<List<Question>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Questions.ToListAsync(cancellationToken);
    }

    #endregion GetAllAsync - End

    #region GetByIdAsync

    public async Task<Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Questions
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    #endregion GetByIdAsync - End

    #region AddAsync

    public async Task AddAsync(Question question, CancellationToken cancellationToken = default)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion AddAsync - End

    #region DeleteAsync

    public async Task DeleteAsync(Question question, CancellationToken cancellationToken = default)
    {
        _context?.Questions.Remove(question);
        await _context?.SaveChangesAsync(cancellationToken);
    }

    #endregion DeleteAsync - End

    #region UpdateAsync

    public async Task UpdateAsync(Question question, CancellationToken cancellationToken = default)
    {
        _context.Update(question);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion UpdateAsync - End
}
