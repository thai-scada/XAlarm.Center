using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Converters;

namespace XAlarm.Center.Domain.Messages.Telegrams;

[JsonConverter(typeof(TelegramMessageConverter))]
public abstract class TelegramMessage : AlarmMessage;