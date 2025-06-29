namespace XAlarm.Center.Domain.Messages.Lines;

public sealed class FlexMessage : LineMessage
{
    public required string AltText { get; set; }
    public required Content Contents { get; init; }
}

public sealed class Content
{
    public string? Type { get; init; }
    public Content? Hero { get; init; }
    public Content? Body { get; init; }
    public Content? Footer { get; init; }
    public string? Style { get; init; }
    public string? Height { get; init; }
    public string? Layout { get; init; }
    public int? Flex { get; init; }
    public string? Text { get; init; }
    public bool? Wrap { get; init; }
    public string? Color { get; init; }
    public string? Weight { get; init; }
    public string? Url { get; init; }
    public string? Size { get; init; }
    public string? Align { get; init; }
    public string? Margin { get; init; }
    public string? Spacing { get; init; }
    public string? AspectRatio { get; init; }
    public string? AspectMode { get; init; }
    public string? OffsetTop { get; set; }
    public string? OffsetBottom { get; set; }
    public string? OffsetStart { get; set; }
    public string? OffsetEnd { get; set; }
    public Action? Action { get; init; }
    public Content[]? Contents { get; init; }
}

public sealed class Action
{
    public required string Type { get; init; }
    public string? Label { get; init; }
    public string? Uri { get; init; }
}