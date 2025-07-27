using ExamTask.Application.Abstractions.Authentication;
using ExamTask.Application.Features.Auth.Login.DTOs;
using ExamTask.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ExamTask.Application.Features.Auth.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Take Roles
        var roles = await _userManager.GetRolesAsync(user);

        // Create token with roles.
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new LoginResponse
        {
            Token = token,
            Role = roles.FirstOrDefault() ?? "Unknown"
        };
    }
}