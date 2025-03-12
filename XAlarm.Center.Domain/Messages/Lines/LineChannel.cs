namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class LineChannel : AlarmChannel
{
    public LineMessage? Message { get; init; }
}