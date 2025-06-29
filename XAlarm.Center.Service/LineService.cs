using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Infrastructure;
using XAlarm.Center.Service.Abstractions;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage

namespace XAlarm.Center.Service;

internal sealed class LineService(ILogger<LineService> logger, ApplicationDbContext dbContext, HttpClient httpClient)
    : ILineService
{
    public async Task<TargetLimitThisMonth> GetTargetLimitThisMonthAsync(Guid projectId, string token)
    {
        try
        {
            var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();
            if (globalSetting is null)
            {
                logger.LogError("GetTargetLimitThisMonthAsync - Global setting not found");
                return new TargetLimitThisMonth(string.Empty, 0);
            }

            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId);
            if (project is null)
            {
                logger.LogError("GetTargetLimitThisMonthAsync - Project not found - {ProjectId}", projectId);
                return new TargetLimitThisMonth(string.Empty, 0);
            }

            logger.LogInformation("GetTargetLimitThisMonthAsync - Line access token - {Token}",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);

            if (project.ProjectOptions.LineOptions.TokenProvider == (int)TokenProviders.CommercialServer)
                return new TargetLimitThisMonth(string.Empty, project.ProjectOptions.LineOptions.TargetLimitThisMonth);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);
            return await httpClient.GetFromJsonAsync<TargetLimitThisMonth>(globalSetting.LineOptions
                .GetTargetLimitThisMonthUrl) ?? new TargetLimitThisMonth(string.Empty, 0);
        }
        catch (Exception ex)
        {
            logger.LogError("GetTargetLimitThisMonthAsync - {Message}", ex.Message);
            return new TargetLimitThisMonth(string.Empty, 0);
        }
    }

    public async Task<NumberOfMessagesSentThisMonth> GetNumberOfMessagesSentThisMonthAsync(Guid projectId, string token)
    {
        try
        {
            var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();
            if (globalSetting is null)
            {
                logger.LogError("GetNumberOfMessagesSentThisMonthAsync - Global setting not found");
                return new NumberOfMessagesSentThisMonth(0);
            }

            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId);
            if (project is null)
            {
                logger.LogError("GetNumberOfMessagesSentThisMonthAsync - Project not found - {ProjectId}", projectId);
                return new NumberOfMessagesSentThisMonth(0);
            }

            logger.LogInformation("GetNumberOfMessagesSentThisMonthAsync - Line access token - {Token}",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);

            if (project.ProjectOptions.LineOptions.TokenProvider == (int)TokenProviders.CommercialServer)
                return new NumberOfMessagesSentThisMonth(project.ProjectOptions.LineOptions
                    .NumberOfMessagesSentThisMonth);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);
            return await httpClient.GetFromJsonAsync<NumberOfMessagesSentThisMonth>(globalSetting.LineOptions
                .GetNumberOfMessagesSentThisMonthUrl) ?? new NumberOfMessagesSentThisMonth(0);
        }
        catch (Exception ex)
        {
            logger.LogError("GetNumberOfMessagesSentThisMonthAsync - {Message}", ex.Message);
            return new NumberOfMessagesSentThisMonth(0);
        }
    }

    public async Task<NumberOfUsersInGroupChat> GetNumberOfUsersInGroupChat(Guid projectId, string groupId,
        string token)
    {
        try
        {
            var globalSetting = await dbContext.GlobalSettings.AsNoTracking().SingleOrDefaultAsync();
            if (globalSetting is null)
            {
                logger.LogError("GetNumberOfUsersInGroupChat - Global setting not found");
                return new NumberOfUsersInGroupChat(0);
            }

            var project = await dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == projectId);
            if (project is null)
            {
                logger.LogError("GetNumberOfUsersInGroupChat - Project not found - {ProjectId}", projectId);
                return new NumberOfUsersInGroupChat(0);
            }

            logger.LogInformation("GetNumberOfUsersInGroupChat - Line access token - {Token}",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                string.IsNullOrEmpty(token) ? project.ProjectOptions.LineOptions.Token : token);
            return await httpClient.GetFromJsonAsync<NumberOfUsersInGroupChat>(globalSetting.LineOptions
                .GetNumberOfUsersInGroupChatUrl.Replace("{groupId}", groupId)) ?? new NumberOfUsersInGroupChat(0);
        }
        catch (Exception ex)
        {
            logger.LogError("GetNumberOfUsersInGroupChat - {Message}", ex.Message);
            return new NumberOfUsersInGroupChat(0);
        }
    }

    public async Task<string> GetQuotaMessageThisMonthAsync(Guid projectId, string groupId, string token, int mode = 0)
    {
        var targetLimitThisMonth = await GetTargetLimitThisMonthAsync(projectId, string.Empty);
        var numberOfMessagesSentThisMonth = await GetNumberOfMessagesSentThisMonthAsync(projectId, string.Empty);
        var numberOfUsersInGroupChat = await GetNumberOfUsersInGroupChat(projectId, groupId, string.Empty);
        return mode == 0
            ? $"{numberOfMessagesSentThisMonth.TotalUsage}/{targetLimitThisMonth.Value} ({Convert.ToInt32(numberOfMessagesSentThisMonth.TotalUsage * 100 / targetLimitThisMonth.Value)}%)"
            : $"{numberOfMessagesSentThisMonth.TotalUsage + numberOfUsersInGroupChat.Count}/{targetLimitThisMonth.Value} ({Convert.ToInt32((numberOfMessagesSentThisMonth.TotalUsage + numberOfUsersInGroupChat.Count) * 100 / targetLimitThisMonth.Value)}%)";
    }
}