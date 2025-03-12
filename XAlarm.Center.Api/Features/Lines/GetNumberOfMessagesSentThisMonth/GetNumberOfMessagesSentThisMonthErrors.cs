using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Lines.GetNumberOfMessagesSentThisMonth;

public static class GetNumberOfMessagesSentThisMonthErrors
{
    public static readonly Error Error = new("GetNumberOfMessagesSentThisMonth.Error",
        "An error occurred while getting number of messages sent this month");
}