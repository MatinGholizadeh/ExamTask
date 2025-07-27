using MediatR;

namespace ExamTask.Application.Features.Auth.Register.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string NationalCode,
    string Email,
    string PhoneNumber,
    string Password,
    string Role // Add Role here
) : IRequest<string>;