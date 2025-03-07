namespace XAlarm.Center.Domain.Abstractions;

public record Error(string Key, string Value)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");
}