namespace XAlarm.Center.Contract.Options;

public abstract record AlarmOptionsDto
{
    public bool Enabled { get; set; }
}