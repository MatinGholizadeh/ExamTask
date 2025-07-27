using ExamTask.Application.Abstractions.Authentication;
using ExamTask.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ExamTask.Application.Features.Auth.Register.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly UserManager<ApplicationUser> _userManager;

    // Constructor to initialize dependencies
    public RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    // Handle registration and token generation
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Create a new ApplicationUser object based on the request
        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            NationalCode = request.NationalCode,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Email,
        };

        // Create the user in the Identity system
        var result = await _userManager.CreateAsync(user, request.Password);

        // Check if user creation succeeded, if not, throw an exception with error details
        if (!result.Succeeded)
            throw new Exception(string.Join(" - ", result.Errors.Select(e => e.Description)));

        // Assign the role received in the request (not hardcoded to "Student")
        var role = request.Role;
        await _userManager.AddToRoleAsync(user, role);

        // Generate JWT token for the user
        var token = _tokenGenerator.GenerateToken(user, new List<string> { role });

        // Return the generated token
        return token;
    }
}