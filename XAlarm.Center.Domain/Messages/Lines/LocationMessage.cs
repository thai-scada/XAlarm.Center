namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class LocationMessage : LineMessage
{
    public string Title { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}