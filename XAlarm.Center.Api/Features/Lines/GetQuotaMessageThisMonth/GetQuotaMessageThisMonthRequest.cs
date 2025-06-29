namespace XAlarm.Center.Api.Features.Lines.GetQuotaMessageThisMonth;

public record GetQuotaMessageThisMonthRequest(Guid ProjectId, string ChatId, string Token, int Mode);