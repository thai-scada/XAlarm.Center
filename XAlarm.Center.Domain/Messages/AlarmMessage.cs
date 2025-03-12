namespace XAlarm.Center.Domain.Messages;

public abstract class AlarmMessage
{
    public string Type { get; set; } = string.Empty;
}