using MediatR;

namespace ExamTask.Application.Features.Auth.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}