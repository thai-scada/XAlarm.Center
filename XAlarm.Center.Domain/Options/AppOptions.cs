using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Options;

public sealed class AppOptions : BaseAppOptions
{
    public RabbitMqOptions RabbitMqOptions { get; init; } = new();
    public EmailOptions EmailOptions { get; init; } = new();
    public LineOptions LineOptions { get; init; } = new();
    public TelegramOptions TelegramOptions { get; init; } = new();
}