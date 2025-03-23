namespace XAlarm.Center.Contract.Abstractions;

public abstract record BaseAppSettingsDto
{
    public string AppName { get; set; } = string.Empty;
    public string AppNameAbbr { get; set; } = string.Empty;
}