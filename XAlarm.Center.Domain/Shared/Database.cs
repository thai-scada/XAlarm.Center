namespace XAlarm.Center.Domain.Shared;

public sealed class Database
{
    public int Type { get; init; }
    public string ConnectionString { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
}