using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Lines.GetQuotaMessageThisMonth;

public static class GetQuotaMessageThisMonthErrors
{
    public static readonly Error Error = new("GetQuotaMessageThisMonth.Error",
        "An error occurred while getting quota message this month");
}