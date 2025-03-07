namespace XAlarm.Center.Shared.Extensions;

public static class StringExtension
{
    public static string Between(this string src, string findFrom, string findTo)
    {
        var start = src.IndexOf(findFrom, StringComparison.Ordinal);
        var to = src.IndexOf(findTo, start + findFrom.Length, StringComparison.Ordinal);
        if (start < 0 || to < 0) return "";
        var s = src.Substring(start + findFrom.Length, to - start - findFrom.Length);
        return s;
    }

    public static List<string> BetweenRange(this string src, string findFrom, string findTo)
    {
        var srcCloned = src.Clone().ToString();
        var listResult = new List<string>();
        var to = 0;

        if (srcCloned is null) return [];
        do
        {
            var start = srcCloned.IndexOf(findFrom, StringComparison.Ordinal);
            if (start < 0) break;
            to = srcCloned.IndexOf(findTo, start + findFrom.Length, StringComparison.Ordinal);
            if (to < 0) continue;
            var sResult = srcCloned.Substring(start + findFrom.Length, to - start - findFrom.Length);
            listResult.Add(sResult);
            srcCloned = srcCloned[to..];
        } while (to >= 0);

        return listResult;
    }
}