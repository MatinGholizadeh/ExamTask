using ExamTask.Application.Features.Auth.Login.DTOs;
using MediatR;

namespace ExamTask.Application.Features.Auth.Login.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}