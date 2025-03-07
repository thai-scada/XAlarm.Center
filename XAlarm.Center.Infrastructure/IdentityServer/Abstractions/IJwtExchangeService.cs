using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.IdentityServer.Abstractions;

public interface IJwtServiceExchange
{
    Task<Result<string>> GetAccessTokenExchangeAsync(string token, CancellationToken cancellationToken = default);
}