using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Lines.GetTargetLimitThisMonth;

public static class GetTargetLimitThisMonthErrors
{
    public static readonly Error Error = new("GetTargetLimitThisMonth.Error",
        "An error occurred while getting target limit this month");
}