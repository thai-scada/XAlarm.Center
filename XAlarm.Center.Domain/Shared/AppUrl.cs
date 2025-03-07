namespace XAlarm.Center.Domain.Shared;

public sealed class AppUrl
{
    public string Protocol { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }

    public string Url => $"{Protocol}://{Host}:{Port}";

    public string UrlLoopback => $"{Protocol}://localhost:{Port}";
}