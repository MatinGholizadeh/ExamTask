namespace ExamTask.Application.Common.Settings;

/// <summary>
/// JWT configuration settings.
/// </summary>
public class JwtSettings
{
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpiryMinutes { get; set; }
}