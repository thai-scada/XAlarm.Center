namespace XAlarm.Center.Domain.Messages.Emails;

public sealed class EmailChannel : AlarmChannel
{
    public EmailMessage? Message { get; init; }
}