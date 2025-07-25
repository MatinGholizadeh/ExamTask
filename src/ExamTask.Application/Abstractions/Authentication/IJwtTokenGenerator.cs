using ExamTask.Domain.Identity;

namespace ExamTask.Application.Abstractions.Authentication;

/// <summary>
/// Contract for generating JWT tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}