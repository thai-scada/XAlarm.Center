using XAlarm.Center.Domain.Abstractions;
using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Infrastructure.IdentityServer.Abstractions;

public interface IUserProfileService
{
    Task<Result<PersonalInfo>> GetUserProfileAsync(string userId, CancellationToken cancellationToken = default);

    Task<Result<string>> UpdateUserProfileAsync(PersonalInfo personalInfo,
        CancellationToken cancellationToken = default);

    Task<Result<string>> DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
}