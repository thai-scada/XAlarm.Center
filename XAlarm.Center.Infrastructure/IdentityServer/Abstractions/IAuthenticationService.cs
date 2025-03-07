using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.IdentityServer.Abstractions;

public interface IAuthenticationService
{
    Task<Result<string>> CreateAccountAsync(string email, string firstName, string lastName,
        CancellationToken cancellationToken = default);

    Task<Result<string>> GetAccountAsync(string email, CancellationToken cancellationToken = default);
}