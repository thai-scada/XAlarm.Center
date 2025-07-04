using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Service.Abstractions;

public interface ILineService
{
    Task<TargetLimitThisMonth> GetTargetLimitThisMonthAsync(Guid projectId, string token);
    Task<NumberOfMessagesSentThisMonth> GetNumberOfMessagesSentThisMonthAsync(Guid projectId, string token);
    Task<NumberOfUsersInGroupChat> GetNumberOfUsersInGroupChat(Guid projectId, string groupId, string token);
    Task<string> GetQuotaMessageThisMonthAsync(Guid projectId, string groupId, string token, int mode = 0);
}