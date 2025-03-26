using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Service.Abstractions;

public interface ILineService
{
    Task<TargetLimitThisMonth> GetTargetLimitThisMonthAsync(Guid projectId, string token);
    Task<NumberOfMessagesSentThisMonth> GetNumberOfMessagesSentThisMonthAsync(Guid projectId, string token);
}