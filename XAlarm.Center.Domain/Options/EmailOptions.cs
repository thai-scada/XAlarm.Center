namespace XAlarm.Center.Domain.Options;

public sealed class EmailOptions : AlarmOptions
{
    public string SmtpHost { get; init; } = string.Empty;
    public int SmtpPort { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Recipients { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
}