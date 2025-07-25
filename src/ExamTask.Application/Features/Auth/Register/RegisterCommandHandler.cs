using MediatR;
using ExamTask.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using ExamTask.Application.Abstractions.Authentication;

namespace ExamTask.Application.Features.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            NationalCode = request.PhoneNumber,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(" - ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Student");

        var token = _tokenGenerator.GenerateToken(user, new List<string> { "Student" });
        return token;
    }
}