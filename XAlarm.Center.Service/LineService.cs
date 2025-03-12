using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Service.Abstractions;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage

namespace XAlarm.Center.Service;

internal sealed class LineService(ApplicationDbContext dbContext, HttpClient httpClient) : ILineService
{
    public async Task<TargetLimitThisMonth> GetTargetLimitThisMonthAsync(Guid projectId)
    {
        try
        {
            var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();
            if (globalSetting is null) return new TargetLimitThisMonth(string.Empty, 0);
            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId);
            if (project is null) return new TargetLimitThisMonth(string.Empty, 0);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", project.ProjectOptions.LineOptions.Token);
            return await httpClient.GetFromJsonAsync<TargetLimitThisMonth>(globalSetting.LineOptions
                .GetTargetLimitThisMonthUrl) ?? new TargetLimitThisMonth(string.Empty, 0);
        }
        catch
        {
            return new TargetLimitThisMonth(string.Empty, 0);
        }
    }

    public async Task<NumberOfMessagesSentThisMonth> GetNumberOfMessagesSentThisMonthAsync(Guid projectId)
    {
        try
        {
            var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();
            if (globalSetting is null) return new NumberOfMessagesSentThisMonth(0);
            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId);
            if (project is null) return new NumberOfMessagesSentThisMonth(0);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", project.ProjectOptions.LineOptions.Token);
            return await httpClient.GetFromJsonAsync<NumberOfMessagesSentThisMonth>(globalSetting.LineOptions
                .GetNumberOfMessagesSentThisMonthUrl) ?? new NumberOfMessagesSentThisMonth(0);
        }
        catch
        {
            return new NumberOfMessagesSentThisMonth(0);
        }
    }
}