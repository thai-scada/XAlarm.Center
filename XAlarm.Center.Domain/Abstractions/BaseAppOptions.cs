using XAlarm.Center.Domain.Shared;

namespace XAlarm.Center.Domain.Abstractions;

public abstract class BaseAppOptions
{
    public string TenantId { get; set; } = string.Empty;
    public string DefaultTenantId { get; set; } = string.Empty;
    public AppUrl AppUrl { get; init; } = new();
    public Database Database { get; init; } = new();
    public string AppName { get; init; } = string.Empty;
    public string AppNameAbbr { get; init; } = string.Empty;
    public string AppConfigName { get; init; } = string.Empty;
    public string AppContentRootPath { get; init; } = string.Empty;
    public int MaxArchiveLogFiles { get; init; }
    public int LogLevel { get; init; } // Trace = 0, Debug = 1, Info = 2, Warn = 3, Error = 4, Fatal = 5
    public int TokenExpiration { get; init; }
    public int RefreshTokenExpiration { get; init; }
    public int VerifyCodeExpiration { get; init; }
    public string TimeZone { get; init; } = "Asia/Bangkok";
    public string Culture { get; init; } = "th-TH";
    public bool ForceApplyMigrations { get; set; }
}