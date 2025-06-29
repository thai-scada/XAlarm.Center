namespace XAlarm.Center.Domain.Options;

public abstract class JobOptions
{
    public bool Enable { get; set; }
    public string CronExpression { get; set; } = string.Empty;
}