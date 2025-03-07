using System.Text;

namespace XAlarm.Center.Shared.Helpers;

public static class ConvertHelper
{
    private static byte[] ConvertArraySegmentToByteArray(IList<ArraySegment<byte>> arraySegments)
    {
        var bytes = new byte[arraySegments.Sum(x => x.Count)];
        var position = 0;

        foreach (var arraySegment in arraySegments)
        {
            if (arraySegment.Array != null)
                Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset, bytes, position, arraySegment.Count);
            position += arraySegment.Count;
        }

        return bytes;
    }

    public static string Unicode2Ms874(string text)
    {
        return Unicode2Ms874(text, true);
    }

    public static string Unicode2Ms874(string text, bool isCheckContainMs874)
    {
        if (!isCheckContainMs874 || !IsContainMs874Thai(text)) return text;
        if (text.Trim().Length <= 0) return text;
        var textBytes = Encoding.Unicode.GetBytes(text);
        var sResult = CodePagesEncodingProvider.Instance.GetEncoding("windows-874")?.GetString(textBytes);
        return sResult?.Replace("\0", "") ?? string.Empty;
    }

    public static string Unicode2Ms874(byte[] textBytes)
    {
        if (textBytes.Length <= 0) return "";
        var result = CodePagesEncodingProvider.Instance.GetEncoding("windows-874")?.GetString(textBytes);
        return result?.Replace("\0", "") ?? string.Empty;
    }

    private static bool IsContainMs874Thai(string text)
    {
        return text.Trim().Length > 0 && text.Any(c => c > 160 && c < 3584);
    }

    private static bool IsContainUnicodeThai(string text)
    {
        return text.Trim().Length > 0 && text.Any(c => c > 3584 && c < 3711);
    }
}