using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Converters;

namespace XAlarm.Center.Domain.Messages;

[JsonConverter(typeof(AlarmMessageConverter))]
public abstract class AlarmMessage
{
    public string Type { get; set; } = string.Empty;
}