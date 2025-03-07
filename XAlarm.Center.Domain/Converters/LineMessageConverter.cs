using System.Text.Json;
using System.Text.Json.Serialization;
using XAlarm.Center.Domain.Messages.Lines;

namespace XAlarm.Center.Domain.Converters;

internal sealed class LineMessageConverter : JsonConverter<LineMessage>
{
    public override bool CanConvert(Type typeToConvert) => typeof(LineMessage).IsAssignableFrom(typeToConvert);

    public override LineMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            "image" => JsonSerializer.Deserialize<ImageMessage>(rawText, options)!,
            "sticker" => JsonSerializer.Deserialize<StickerMessage>(rawText, options)!,
            "location" => JsonSerializer.Deserialize<LocationMessage>(rawText, options)!,
            "flex" => JsonSerializer.Deserialize<FlexMessage>(rawText, options)!,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, LineMessage value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case TextMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case ImageMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case StickerMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case LocationMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
            case FlexMessage message:
                JsonSerializer.Serialize(writer, message, options);
                break;
        }
    }
}