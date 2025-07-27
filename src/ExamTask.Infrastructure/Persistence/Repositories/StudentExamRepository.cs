using ExamTask.Application.Abstractions.Persistence.Repositories;
using ExamTask.Domain.Entities;
using ExamTask.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamTask.Infrastructure.Persistence.Repositories;

public class StudentExamRepository : IStudentExamRepository
{
    private readonly IdentityDbContext _context;

    public StudentExamRepository(IdentityDbContext context)
    {
        _context = context;
    }

    #region ExistAsync

    public async Task<bool> ExistsAsync(int studentExamId, CancellationToken cancellationToken = default)
    {
        return await _context.StudentExams
            .AnyAsync(se => se.Id == studentExamId, cancellationToken);
    }

    #endregion ExistAsync - End

    #region GetAllAsync

    public async Task<List<StudentExam>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.StudentExams.ToListAsync(cancellationToken);
    }

    #endregion GetAllAsync - End

    #region GetByIdAsync

    public async Task<StudentExam?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.StudentExams
            .FirstOrDefaultAsync(se => se.Id == id, cancellationToken);
    }

    #endregion GetByIdAsync - End

    #region AddAsync

    public async Task AddAsync(StudentExam studentExam, CancellationToken cancellationToken = default)
    {
        _context.StudentExams.Add(studentExam);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion AddAsync - End

    #region DeleteAsync

    public async Task DeleteAsync(StudentExam studentExam, CancellationToken cancellationToken = default)
    {
        _context.StudentExams.Remove(studentExam);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion DeleteAsync - End

    #region UpdateAsync

    public async Task UpdateAsync(StudentExam studentExam, CancellationToken cancellationToken = default)
    {
        _context.StudentExams.Update(studentExam);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion UpdateAsync - End
}
