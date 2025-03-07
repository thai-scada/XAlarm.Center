using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.Seeds;

public static class XAlarmCenterSeed
{
    public static void Seed<T>(DbContext dbContext, IEnumerable<T> entities, string tableName) where T : Entity
    {
        var tablesCount = dbContext.Database.SqlQuery<int>(GetCheckTableExistSql(tableName)).SingleOrDefault();
        if (tablesCount == 0) return;
        var rowsCount = dbContext.Set<T>().Any();
        if (rowsCount) return;
        foreach (var entity in entities)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }
    }

    public static async Task SeedAsync<T>(DbContext dbContext, IEnumerable<T> entities, string tableName,
        CancellationToken cancellationToken) where T : Entity
    {
        var tablesCount = await dbContext.Database.SqlQuery<int>(GetCheckTableExistSql(tableName))
            .SingleOrDefaultAsync(cancellationToken);
        if (tablesCount == 0) return;
        var rowsCount = await dbContext.Set<T>().AnyAsync(cancellationToken);
        if (rowsCount) return;
        foreach (var entity in entities)
        {
            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static FormattableString GetCheckTableExistSql(string tableName, int databaseType = 0)
    {
        return databaseType switch
        {
            _ =>
                $"SELECT COUNT(name) as \"Value\" FROM sqlite_master WHERE type = 'table' AND name = {tableName}"
        };
    }
}