using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Infrastructure;

// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataQuery

namespace XAlarm.Center.Service.Jobs;

public class ResetMessageQuotaJob(ILogger<ResetMessageQuotaJob> logger, ApplicationDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Job '{JobName}' started at: {Now}", context.JobDetail.Key.Name, DateTime.Now);

        var projects = await dbContext.Projects.AsNoTracking().ToListAsync();
        if (projects is not [])
            foreach (var project in projects.Where(x =>
                         x.ProjectOptions.LineOptions.TokenProvider == (int)TokenProviders.CommercialServer))
            {
                project.ProjectOptions.LineOptions.NumberOfMessagesSentThisMonth = 0;
                await dbContext.Projects.AsNoTracking().Where(x => x.Id == project.Id)
                    .ExecuteUpdateAsync(x => x.SetProperty(y => y.ProjectOptions, project.ProjectOptions));
            }
    }
}