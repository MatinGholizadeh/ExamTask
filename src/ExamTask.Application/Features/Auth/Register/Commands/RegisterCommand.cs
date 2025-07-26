using MediatR;

namespace ExamTask.Application.Features.Auth.Register.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string PasswordHash
) : IRequest<string>;