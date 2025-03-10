using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Domain.Settings;

public sealed class GlobalSetting : Entity
{
    public LineOptions LineOptions { get; init; } = new();
    public TelegramOptions TelegramOptions { get; init; } = new();
    public EmailOptions EmailOptions { get; init; } = new();
    public SmsOptions SmsOptions { get; init; } = new();
}