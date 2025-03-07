using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace XAlarm.Center.Shared.Helpers;

public static class JsonHelper
{
    public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    
    public static readonly JsonSerializerOptions IgnoreNullJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static readonly JsonSerializerOptions IndentedJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };
}