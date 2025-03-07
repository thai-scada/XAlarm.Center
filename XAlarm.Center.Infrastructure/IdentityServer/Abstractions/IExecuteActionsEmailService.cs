using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.IdentityServer.Abstractions;

public interface IExecuteActionsEmailService
{
    Task<Result<string>> ExecuteAccountNotFullySetUpAsync(string email,
        CancellationToken cancellationToken = default);

    Task<Result<string>> ExecuteForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
}