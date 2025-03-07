using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Settings;

namespace XAlarm.Center.Infrastructure.Seeds;

public static class GlobalSettingSeed
{
    private const string TableName = "global_settings";

    public static IEnumerable<GlobalSetting> GetEntities()
    {
        var ids = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "..", "assets", "migrations",
            "global-setting-id.txt"));

        return
        [
            new GlobalSetting(Guid.Parse(ids[0]), new GeneralSetting())
        ];
    }

    public static void Seed(DbContext dbContext, IEnumerable<GlobalSetting> entities)
    {
        XAlarmCenterSeed.Seed(dbContext, entities, TableName);
    }

    public static async Task SeedAsync(DbContext dbContext, IEnumerable<GlobalSetting> entities,
        CancellationToken cancellationToken)
    {
        await XAlarmCenterSeed.SeedAsync(dbContext, entities, TableName, cancellationToken);
    }
}