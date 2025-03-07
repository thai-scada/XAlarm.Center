using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Converters;

namespace XAlarm.Center.Domain.Messages.Lines;

[JsonConverter(typeof(LineMessageConverter))]
public abstract class LineMessage
{
    public string Type { get; set; } = string.Empty;
}