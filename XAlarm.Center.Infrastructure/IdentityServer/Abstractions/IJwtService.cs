using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Projects;
using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Infrastructure.IdentityServer.Abstractions;

public interface IJwtService
{
    Task<Result<TokenInfo>> GetAccessTokenAsync(string email, string password, Project project,
        CancellationToken cancellationToken = default);

    Task<Result<TokenInfo>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}