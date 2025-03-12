namespace XAlarm.Center.Domain.Messages.Sms;

public sealed class SmsChannel : AlarmChannel
{
    public SmsMessage? Message { get; init; }
}