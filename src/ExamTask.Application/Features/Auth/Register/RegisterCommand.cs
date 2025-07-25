using MediatR;

namespace ExamTask.Application.Features.Auth.Register;

public record RegisterCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password
) : IRequest<string>;