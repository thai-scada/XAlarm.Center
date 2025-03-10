using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Domain.Projects;

public sealed class ProjectOptions
{
    public EmailOptions EmailOptions { get; init; } = new();
    public LineOptions LineOptions { get; init; } = new();
    public TelegramOptions TelegramOptions { get; init; } = new();
}