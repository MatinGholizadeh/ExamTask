namespace ExamTask.Application.Features.Auth.Login;

public class LoginResponse
{
    public string Token { get; set; } = default!;
    public string Role { get; set; } = default!;
}