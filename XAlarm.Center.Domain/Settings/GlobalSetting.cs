using System.Diagnostics.CodeAnalysis;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Domain.Settings;

public sealed class GlobalSetting : Entity
{
    public GlobalSetting()
    {
    }

    [SetsRequiredMembers]
    public GlobalSetting(Guid id, GeneralSetting generalSetting)
    {
        Id = id;
        GeneralSetting = generalSetting;
    }

    public GeneralSetting GeneralSetting { get; init; } = new();
}