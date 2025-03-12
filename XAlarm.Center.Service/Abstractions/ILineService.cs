using XAlarm.Center.Domain.Options;

namespace XAlarm.Center.Service.Abstractions;

public interface ILineService
{
    Task<TargetLimitThisMonth> GetTargetLimitThisMonthAsync(Guid projectId);
    Task<NumberOfMessagesSentThisMonth> GetNumberOfMessagesSentThisMonthAsync(Guid projectId);
}