using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Domain.Entities;
using ExamTask.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.Infrastructure.Persistence.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly IdentityDbContext _context;

    public ExamRepository(IdentityDbContext context)
    {
        _context = context;
    }

    #region ExistsAsync

    public async Task<bool> ExistsAsync(int examId, CancellationToken cancellationToken = default)
    {
        return await _context.Exams.AnyAsync(e => e.Id == examId, cancellationToken);
    }

    #endregion ExistsAsync - End

    #region GetAllAsync

    public async Task<List<Exam>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Exams.ToListAsync(cancellationToken);
    }

    #endregion GetAllAsync - End

    #region GetByIdAsync

    public async Task<Exam?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Exams
            .Include(e => e.Questions)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    #endregion GetByIdAsync - End

    #region AddAsync

    public async Task AddAsync(Exam exam, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(exam, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion AddAsync - End

    #region DeleteAsync

    public async Task DeleteAsync(Exam exam, CancellationToken cancellationToken = default)
    {
        _context.Exams.Remove(exam);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion DeleteAsync - End

    #region UpdateAsync

    public async Task UpdateAsync(Exam exam, CancellationToken cancellationToken = default)
    {
        _context.Exams.Update(exam);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion UpdateAsync - End

}
