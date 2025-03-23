using XAlarm.Center.Contract.Options;

namespace XAlarm.Center.Contract.Projects;

public sealed record ProjectOptionsDto
{
    public EmailOptionsDto EmailOptions { get; init; } = new();
    public LineOptionsDto LineOptions { get; init; } = new();
    public TelegramOptionsDto TelegramOptions { get; init; } = new();
}