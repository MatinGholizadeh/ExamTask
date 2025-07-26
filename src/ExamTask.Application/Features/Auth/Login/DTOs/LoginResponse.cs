namespace ExamTask.Application.Features.Auth.Login.DTOs;

public class LoginResponse
{
    public string Token { get; set; } = default!;
    public string Role { get; set; } = default!;
}