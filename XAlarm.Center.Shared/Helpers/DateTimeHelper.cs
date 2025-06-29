using System.Globalization;

namespace XAlarm.Center.Shared.Helpers;

public static class DateTimeHelper
{
    public static DateTime DateTh2DateEn(DateTime dtDateTime, CultureInfo currentCulture)
    {
        if (currentCulture.CompareInfo.Name.Equals("th-TH")) dtDateTime = dtDateTime.AddYears(-543);

        return dtDateTime;
    }

    public static DateTime DateEn2DateTh(DateTime dtDateTime)
    {
        dtDateTime = dtDateTime.AddYears(543);
        return dtDateTime;
    }

    public static DateTime JulianDate2DateTime(double nJulianDate)
    {
        var l = (long)nJulianDate + 68569;
        var n = 4 * l / 146097;
        l = l - (146097 * n + 3) / 4;
        var I = 4000 * (l + 1) / 1461001;
        l = l - 1461 * I / 4 + 31;
        var j = 80 * l / 2447;
        var day = (int)(l - 2447 * j / 80);
        l = j / 11;
        var month = (int)(j + 2 - 12 * l);
        var year = (int)(100 * (n - 49) + I + l);

        var dtDateTime = new DateTime(year, month, day).AddDays(1);

        return dtDateTime;
    }

    public static DateTime? JulianDate2DateTimeNullable(double nJulianDate)
    {
        var l = (long)nJulianDate + 68569;
        var n = 4 * l / 146097;
        l = l - (146097 * n + 3) / 4;
        var I = 4000 * (l + 1) / 1461001;
        l = l - 1461 * I / 4 + 31;
        var j = 80 * l / 2447;
        var day = (int)(l - 2447 * j / 80);
        l = j / 11;
        var month = (int)(j + 2 - 12 * l);
        var year = (int)(100 * (n - 49) + I + l);

        var dtDateTime = new DateTime(year, month, day).AddDays(1);

        return dtDateTime;
    }

    public static double DateTime2JulianDate(DateTime dtDateTime)
    {
        return dtDateTime.ToOADate() + 2415018.5;
    }

    public static double DateTime2JulianDate(DateTime dtDateTime, int nOffset)
    {
        return dtDateTime.ToOADate() + 2415018.5 + nOffset;
    }

    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
    {
        var defaultCultureInfo = CultureInfo.CurrentCulture;
        var firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
        return GetFirstDayOfWeek(dayInWeek, firstDay);
    }

    public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
    {
        var firstDayInWeek = dayInWeek.Date;
        while (firstDayInWeek.DayOfWeek != firstDay) firstDayInWeek = firstDayInWeek.AddDays(-1);

        return firstDayInWeek;
    }

    public static DateTime GetFirstDayOfMonth(DateTime dtDate)
    {
        var dtFrom = dtDate;
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
        return dtFrom;
    }

    public static DateTime GetLastDayOfMonth(DateTime dtDate)
    {
        var dtTo = dtDate;
        dtTo = dtTo.AddMonths(1);
        dtTo = dtTo.AddDays(-dtTo.Day);
        return dtTo;
    }

    public static DateTime GetFirstDayOfQuarterOfYear(DateTime dtDate)
    {
        switch (dtDate.Month)
        {
            case 1:
            case 2:
            case 3:
                return new DateTime(dtDate.Year, 1, 1);
            case 4:
            case 5:
            case 6:
                return new DateTime(dtDate.Year, 4, 1);
            case 7:
            case 8:
            case 9:
                return new DateTime(dtDate.Year, 7, 1);
            default:
                return new DateTime(dtDate.Year, 10, 1);
        }
    }

    public static DateTime GetLastDayOfQuarterOfYear(DateTime dtDate)
    {
        switch (dtDate.Month)
        {
            case 1:
            case 2:
            case 3:
                return new DateTime(dtDate.Year, 3, 31);
            case 4:
            case 5:
            case 6:
                return new DateTime(dtDate.Year, 6, 30);
            case 7:
            case 8:
            case 9:
                return new DateTime(dtDate.Year, 9, 30);
            default:
                return new DateTime(dtDate.Year, 12, 31);
        }
    }

    public static DateTime GetFirstDayOfBiMonth(DateTime dtDate)
    {
        switch (dtDate.Month)
        {
            case 1:
            case 2:
                return new DateTime(dtDate.Year, 1, 1);
            case 3:
            case 4:
                return new DateTime(dtDate.Year, 3, 1);
            case 5:
            case 6:
                return new DateTime(dtDate.Year, 5, 1);
            case 7:
            case 8:
                return new DateTime(dtDate.Year, 7, 1);
            case 9:
            case 10:
                return new DateTime(dtDate.Year, 9, 1);
            default:
                return new DateTime(dtDate.Year, 11, 1);
        }
    }

    public static DateTime GetFirstDayOfYear(DateTime dtDate)
    {
        return new DateTime(dtDate.Year, 1, 1);
    }

    public static DateTime GetLastDayOfYear(DateTime dtDate)
    {
        return new DateTime(dtDate.Year, 12, 31);
    }

    public static int GetWeekNumberOfYear(DateTime dtPassed)
    {
        var ciCurr = new CultureInfo("en-US");
        var weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        return weekNum;
    }

    public static string GetWeekTextOfYear(DateTime dtPassed)
    {
        var nWeekNum = GetWeekNumberOfYear(dtPassed);
        var sYear = dtPassed.Year.ToString().Substring(2, 1);
        if (dtPassed.Month == 1 && nWeekNum > 40) sYear = (dtPassed.Year - 1).ToString().Substring(3, 1);

        return sYear + nWeekNum.ToString().PadLeft(2, '0');
    }

    public static int GetTotalPointBetweenDateTime(int nIntervalType, DateTime dtFromDateTime, DateTime dtToDateTime)
    {
        return nIntervalType switch
        {
            0 => // second
                (int)Math.Floor((dtToDateTime - dtFromDateTime).TotalSeconds),
            _ => (int)Math.Floor((dtToDateTime - dtFromDateTime).TotalMinutes)
        };
    }

    public static string DateTimeToString(DateTime dtDataDateTime)
    {
        return dtDataDateTime.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
    }

    public static string DateTimeToString(DateTime dtDataDateTime, CultureInfo cultureInfo)
    {
        return dtDataDateTime.ToString("dd/MM/yyyy HH:mm:ss", cultureInfo);
    }

    public static string DateTimeToString(DateTime? dtDataDateTime)
    {
        return dtDataDateTime is not null
            ? dtDataDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"))
            : "";
    }

    public static string DateTimeToString(DateTime? dtDataDateTime, CultureInfo cultureInfo)
    {
        return dtDataDateTime is not null ? dtDataDateTime.Value.ToString("dd/MM/yyyy HH:mm:ss", cultureInfo) : "";
    }

    public static string DateTimeToStringSql(DateTime dtDataDateTime)
    {
        return dtDataDateTime.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));
    }

    public static string DateTimeToStringSql(DateTime? dtDataDateTime)
    {
        return dtDataDateTime is not null
            ? dtDataDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"))
            : "";
    }

    public static string DateTimeMillisecondsToStringSql(DateTime? dtDataDateTime)
    {
        return dtDataDateTime is not null
            ? dtDataDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US"))
            : "";
    }

    public static string DateTimeToStringSqlOracle(DateTime? dtDataDateTime)
    {
        return dtDataDateTime is not null
            ? "TO_DATE('" + dtDataDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) +
              "', 'YYYY-MM-DD HH24:MI:SS')"
            : "";
    }

    public static string DateTimeMillisecondsToStringSqlOracle(DateTime? dtDataDateTime)
    {
        return dtDataDateTime is not null
            ? "TO_TIMESTAMP('" + dtDataDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff", new CultureInfo("en-US")) +
              "', 'YYYY-MM-DD HH24:MI:SS.FF3')"
            : "";
    }

    public static string DateTimeToString(DateTime dtDataDateTime, string sFormat)
    {
        return dtDataDateTime.ToString(sFormat, new CultureInfo("en-US"));
    }

    public static string DateTimeToString(DateTime dtDataDateTime, string sFormat, CultureInfo cultureInfo)
    {
        return dtDataDateTime.ToString(sFormat, cultureInfo);
    }

    public static string DateTimeToString(DateTime? dtDataDateTime, string sFormat)
    {
        return dtDataDateTime is not null ? dtDataDateTime.Value.ToString(sFormat, new CultureInfo("en-US")) : "";
    }

    public static string DateTimeToString(DateTime? dtDataDateTime, string sFormat, CultureInfo cultureInfo)
    {
        return dtDataDateTime is not null ? dtDataDateTime.Value.ToString(sFormat, cultureInfo) : "";
    }

    public static string DateTimeToString(DateTime dtDataDateTime, string sFormat, string sCultureInfo)
    {
        return dtDataDateTime.ToString(sFormat, new CultureInfo(sCultureInfo));
    }

    public static string DateTimeToString(DateTime? dtDataDateTime, string sFormat, string sCultureInfo)
    {
        return dtDataDateTime is not null ? dtDataDateTime.Value.ToString(sFormat, new CultureInfo(sCultureInfo)) : "";
    }

    public static DateTime StringToDateTime(string dateTimeText, string format)
    {
        return DateTime.TryParseExact(dateTimeText, format, new CultureInfo("en-US"), DateTimeStyles.None,
            out var dateTimeParsed)
            ? dateTimeParsed
            : DateTime.MinValue;
    }

    public static DateTime StringToDateTimeUtc(string dateTimeText, string format)
    {
        return DateTime.TryParseExact(dateTimeText, format, new CultureInfo("en-US"), DateTimeStyles.None,
            out var dateTimeParsed)
            ? dateTimeParsed.ToUniversalTime()
            : DateTime.MinValue.ToUniversalTime();
    }

    public static DateTime StringToDateTimeUtc(string dateTimeText, string format, bool validateMillisecond)
    {
        if (validateMillisecond && dateTimeText.Contains('.'))
        {
            var millisecond = dateTimeText.Split('.')[1];
            format = format + "." + new string('f', millisecond.Length);
        }

        return DateTime.TryParseExact(dateTimeText, format, new CultureInfo("en-US"), DateTimeStyles.None,
            out var dateTimeParsed)
            ? dateTimeParsed.ToUniversalTime()
            : DateTime.MinValue.ToUniversalTime();
    }

    public static string DateTimeUtcToString(DateTime? dateTime, string format, string timeZone)
    {
        return DateTimeUtcToString(dateTime, format, new CultureInfo("en-US"), timeZone);
    }

    public static string DateTimeUtcToString(DateTime? dateTime, string format, CultureInfo cultureInfo,
        string timeZone)
    {
        var cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        return dateTime is not null
            ? TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, cstZone).ToString(format, cultureInfo)
            : string.Empty;
    }

    public static DateTime GetToday()
    {
        return StringToDateTime(DateTimeToString(DateTime.Today, "yyyyMMdd"), "yyyyMMdd");
    }

    public static DateTime GetBeginOfToday()
    {
        return DateTime.Today;
    }

    public static DateTime GetEndOfToday()
    {
        return DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
    }

    public static DateTime GetBeginOfCurrentHour(DateTime dtNow, int nHourInterval)
    {
        return dtNow.Trim(TimeSpan.TicksPerHour).AddHours(nHourInterval);
    }

    public static DateTime GetEndOfCurrentHour(DateTime dtNow, int nHourInterval)
    {
        return dtNow.Trim(TimeSpan.TicksPerHour).AddHours(nHourInterval).AddMinutes(59).AddSeconds(59);
    }

    public static DateTime Trim(this DateTime date, long roundTicks)
    {
        return new DateTime(date.Ticks - date.Ticks % roundTicks);
    }

    public static List<DateTime> GetDateRange(DateTime dtFrom, DateTime dtTo)
    {
        return Enumerable.Range(0, dtTo.Subtract(dtFrom).Days + 1).Select(d => dtFrom.AddDays(d)).ToList();
    }

    public static bool IsCurrentTimeInRange(TimeSpan tsStart, TimeSpan tsEnd)
    {
        var tsNow = DateTime.Now.TimeOfDay;

        if (tsStart <= tsEnd)
        {
            // start and stop times are in the same day
            if (tsNow >= tsStart && tsNow <= tsEnd) return true;
        }
        else
        {
            // start and stop times are in different days
            if (tsNow >= tsStart || tsNow <= tsEnd) return true;
        }

        return false;
    }

    public static bool IsTimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
    {
        // convert datetime to a TimeSpan
        var now = datetime.TimeOfDay;
        // see if start comes before end
        if (start < end)
            return start <= now && now <= end;
        // start is after end, so do the inverse comparison
        return !(end < now && now < start);
    }

    public static DateTime FromUnixTime(double unixTime)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddMilliseconds(unixTime);
    }

    public static List<Tuple<DateTime, DateTime>> GetWeeksOfMonth(DateTime dtDateTime, DayOfWeek startDayOfWeek)
    {
        var calendar = CultureInfo.CreateSpecificCulture("en-US").Calendar;

        var daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(dtDateTime.Year, dtDateTime.Month));

        return daysInMonth.Select(day => new DateTime(dtDateTime.Year, dtDateTime.Month, day))
            .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, startDayOfWeek))
            .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last())).ToList();
    }

    public static int ValidateShift(TimeSpan tsStart, TimeSpan tsEnd)
    {
        var tsNow = DateTime.Now.TimeOfDay;

        if (tsStart <= tsEnd)
        {
            // start and stop times are in the same day
            if (tsNow >= tsStart && tsNow <= tsEnd) return 1; // ok
        }
        else
        {
            // start and stop times are in different days
            if (tsNow >= tsStart) return 2; // ok, before midnight

            if (tsNow <= tsEnd) return 3; // ok, after midnight then minus 1 day
        }

        if (tsNow < tsStart) return -1; // not yet time

        return -2; // overdue
    }

    public static string GetQuarterOfYearText(DateTime dtDateTime)
    {
        return "Q" + (dtDateTime.Month + 2) / 3 + " " + DateTimeToString(dtDateTime, "yyyy");
    }

    public static string GetBiMonthlyText(DateTime dtDateTime)
    {
        return DateTimeToString(dtDateTime, "MMM") + "-" + DateTimeToString(dtDateTime.AddMonths(1), "MMM") + " " +
               DateTimeToString(dtDateTime, "yyyy");
    }

    public static string SecondToMinuteString(int nTotalSeconds)
    {
        var nSeconds = nTotalSeconds % 60;
        var nMinutes = nTotalSeconds / 60;

        return nMinutes + (nSeconds != 0 ? ":" + nSeconds.ToString().PadLeft(2, '0') : "");
    }

    public static string SecondToMinuteStringFull(int nTotalSeconds)
    {
        var nSeconds = nTotalSeconds % 60;
        var nMinutes = nTotalSeconds / 60;

        return nMinutes + " minutes " + nSeconds + " seconds";
    }

    public static DateTime GetDateTimeNowWithMilliseconds()
    {
        var dtNow = DateTime.Now;
        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second,
            dtNow.Millisecond);
    }

    public static DateTime ConvertByTimeZone(DateTime dateTime, string timeZoneId)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
    }
}