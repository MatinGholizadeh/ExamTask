using MediatR;
using ExamTask.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using ExamTask.Application.Abstractions.Authentication;

namespace ExamTask.Application.Features.Auth.Register.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = request.PasswordHash,
        };

        var result = await _userManager.CreateAsync(user, request.PasswordHash);

        if (!result.Succeeded)
            throw new Exception(string.Join(" - ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Student");

        var token = _tokenGenerator.GenerateToken(user, new List<string> { "Student" });
        return token;
    }
}