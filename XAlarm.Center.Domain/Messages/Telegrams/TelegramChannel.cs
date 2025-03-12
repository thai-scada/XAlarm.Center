namespace XAlarm.Center.Domain.Messages.Telegrams;

public sealed class TelegramChannel : AlarmChannel
{
    public TelegramMessage? Message { get; init; }
}