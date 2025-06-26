namespace XAlarm.Center.Domain.Options;

public abstract class AlarmOptions
{
    public bool Enabled { get; set; }
    public int TokenProvider { get; set; }
}