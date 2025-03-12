using System.Text.Json;
using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Messages.Emails;
using XAlarm.Center.Domain.Messages.Lines;
using XAlarm.Center.Domain.Messages.Sms;
using XAlarm.Center.Domain.Messages.Telegrams;

namespace XAlarm.Center.Domain.Converters;

internal sealed class AlarmChannelConverter : JsonConverter<AlarmChannel>
{
    public override bool CanConvert(Type typeToConvert) => typeof(AlarmChannel).IsAssignableFrom(typeToConvert);

    public override AlarmChannel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        if (!jsonDocument.RootElement.TryGetProperty("type", out var typeProperty))
            throw new JsonException();

        var rawText = jsonDocument.RootElement.GetRawText();
        return typeProperty.GetString() switch
        {
            "line" => JsonSerializer.Deserialize<LineChannel>(rawText, options)!,
            "telegram" => JsonSerializer.Deserialize<TelegramChannel>(rawText, options)!,
            "email" => JsonSerializer.Deserialize<EmailChannel>(rawText, options)!,
            "sms" => JsonSerializer.Deserialize<SmsChannel>(rawText, options)!,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, AlarmChannel value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case LineChannel message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case TelegramChannel message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case EmailChannel message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case SmsChannel message:
                JsonSerializer.Serialize(writer, message, options);
                break;
        }
    }
}