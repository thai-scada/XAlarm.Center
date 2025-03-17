using System.Text.Json;
using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Messages.Telegrams;

namespace XAlarm.Center.Domain.Converters;

internal sealed class TelegramMessageConverter : JsonConverter<TelegramMessage>
{
    public override bool CanConvert(Type typeToConvert) => typeof(TelegramMessage).IsAssignableFrom(typeToConvert);

    public override TelegramMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        if (!jsonDocument.RootElement.TryGetProperty("type", out var typeProperty))
            throw new JsonException();

        var rawText = jsonDocument.RootElement.GetRawText();
        return typeProperty.GetString() switch
        {
            "text" => JsonSerializer.Deserialize<TextMessage>(rawText, options)!,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, TelegramMessage value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case TextMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
        }
    }
}