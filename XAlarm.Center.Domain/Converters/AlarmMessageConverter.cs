using System.Text.Json;
using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Emails;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Messages.Sms;
using XAlarm.Center.Domain.Messages.Telegrams;

namespace XAlarm.Center.Domain.Converters;

internal sealed class AlarmMessageConverter : JsonConverter<AlarmMessage>
{
    public override bool CanConvert(Type typeToConvert) => typeof(AlarmMessage).IsAssignableFrom(typeToConvert);

    public override AlarmMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        if (!jsonDocument.RootElement.TryGetProperty("type", out var typeProperty))
            throw new JsonException();

        var rawText = jsonDocument.RootElement.GetRawText();
        return typeProperty.GetString() switch
        {
            "line" => JsonSerializer.Deserialize<LineMessage>(rawText, options)!,
            "telegram" => JsonSerializer.Deserialize<TelegramMessage>(rawText, options)!,
            "email" => JsonSerializer.Deserialize<EmailMessage>(rawText, options)!,
            "sms" => JsonSerializer.Deserialize<SmsMessage>(rawText, options)!,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, AlarmMessage value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case LineMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case TelegramMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case EmailMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case SmsMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
        }
    }
}