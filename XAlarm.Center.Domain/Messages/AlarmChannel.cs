using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Converters;

namespace XAlarm.Center.Domain.Messages;

[JsonConverter(typeof(AlarmChannelConverter))]
public abstract class AlarmChannel
{
    public string Type { get; set; } = string.Empty;
}