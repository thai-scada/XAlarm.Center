using Microsoft.EntityFrameworkCore;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Projects;

namespace XAlarm.Center.Infrastructure.Seeds;

public static class ProjectSeed
{
    private const string TableName = "projects";

    public static IEnumerable<Project> GetEntities()
    {
        var ids = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "..", "assets", "data", "project-id.txt"));
        var groupIds =
            File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "..", "assets", "data", "project-group-id.txt"));
        var names = File.ReadAllLines(
            Path.Combine(AppContext.BaseDirectory, "..", "assets", "data", "project-name.txt"));

        return
        [
            new Project
            {
                Id = Guid.Parse(ids[0]),
                ProjectId = Guid.Parse(groupIds[0]),
                ProjectName = names[0],
                ProjectGroupId = Guid.Parse(groupIds[0]),
                DongleId = string.Empty,
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.NonCommercialServer,
                        Token =
                            "XsHQB267fv3aQvspwnL+3iFIMkF+JT7uJZ0RFYgq283p0t0bZbUoWsh66/aT++B8f7QcVdIrL4txhNOEGKKSvUfyZxZF3ye+suCemNtdZfPTf5RDU/1PdmGociriE1Q2BPJXAAyqZlErIDlF9TAR9QdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = "8195040424:AAH4yq8EB4yY09ffeTKWh4JFkWq_5dl85HM",
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[1]),
                ProjectId = Guid.Parse(groupIds[1]),
                ProjectName = names[1],
                ProjectGroupId = Guid.Parse(groupIds[1]),
                DongleId = string.Empty,
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.Client,
                        Token =
                            "bwZMJS7dZYqLwfvzgOO9+02R41LAc4BMoB/yqsbvEtCJlsiuKWeAGYKdwhaZvzFnMLHZUw7ZHL+63HjzyxiFftjsC+0zniLR6CbH+V/mr3rVn2XHf/M9krKYZxvmV58nvIDcPCB8idfEHbmNVMR25QdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = "8195040424:AAH4yq8EB4yY09ffeTKWh4JFkWq_5dl85HM"
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[2]),
                ProjectId = Guid.Parse(groupIds[2]),
                ProjectName = names[2],
                ProjectGroupId = Guid.Parse(groupIds[2]),
                DongleId = "8090ac0e,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.NonCommercialServer,
                        Token =
                            "pV4uLfY6yo5pNObo432hUtB0SMN8p9/OsAj1E3BuOaVXwSO+gXmw0iC+3x2gRfueUpOAd2UfKL6c0dt6yPXA9CVXPaWCa6kc6pfVKuhwPK6KOwLW1VwWAL95GMzrl+GHSxadm6PQTIODErKyP+UmbwdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[3]),
                ProjectId = Guid.Parse(groupIds[3]),
                ProjectName = names[3],
                ProjectGroupId = Guid.Parse(groupIds[3]),
                DongleId = "8090ac03,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.NonCommercialServer,
                        Token =
                            "6tvc9YboYf/AFO79TY5qzWfuYiFE4JDUMMrtu8OGeXoOlYsjZallo8YGAsC9wH/7tkSYE7BvRe1Zh+mz1RwdhY6TGmtDhayvVUDZNPiB9bsBqrJ/MoVEIaPYOcChXJIXCKdM5kgdUaJreKN8FxQi+AdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[4]),
                ProjectId = Guid.Parse(groupIds[4]),
                ProjectName = names[4],
                ProjectGroupId = Guid.Parse(groupIds[4]),
                DongleId = "1f916e5a,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.Client,
                        Token = string.Empty
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[5]),
                ProjectId = Guid.Parse(groupIds[5]),
                ProjectName = names[5],
                ProjectGroupId = Guid.Parse(groupIds[5]),
                DongleId = "80822c00,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.Client,
                        Token = string.Empty
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[6]),
                ProjectId = Guid.Parse(groupIds[6]),
                ProjectName = names[6],
                ProjectGroupId = Guid.Parse(groupIds[6]),
                DongleId = "1234abcd,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.CommercialServer,
                        NumberOfMessagesSentThisMonth = 0,
                        TargetLimitThisMonth = 7500,
                        Token =
                            "XsHQB267fv3aQvspwnL+3iFIMkF+JT7uJZ0RFYgq283p0t0bZbUoWsh66/aT++B8f7QcVdIrL4txhNOEGKKSvUfyZxZF3ye+suCemNtdZfPTf5RDU/1PdmGociriE1Q2BPJXAAyqZlErIDlF9TAR9QdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = "8195040424:AAH4yq8EB4yY09ffeTKWh4JFkWq_5dl85HM",
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[7]),
                ProjectId = Guid.Parse(groupIds[7]),
                ProjectName = names[7],
                ProjectGroupId = Guid.Parse(groupIds[7]),
                DongleId = "8071a80d,18ab0536",
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.Client,
                        Token =
                            "ecXXdbZ4hC2Mg6cBM0PIVYVdt3YgdirsY6cr/+dcHeCv0rVs4D/8XtoowP5cbn3auC0Ud7I8UgkmMrd2n6CJsD8mJvcx/4R0IK7VG11e4GtNx49cJlrdrJ6NvYUygSOBjWOW2NamFslb7asf8+mRTgdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[8]),
                ProjectId = Guid.Parse(groupIds[8]),
                ProjectName = names[8],
                ProjectGroupId = Guid.Parse(groupIds[8]),
                DongleId = string.Empty,
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.CommercialServer,
                        NumberOfMessagesSentThisMonth = 0,
                        TargetLimitThisMonth = 7500,
                        Token =
                            "XsHQB267fv3aQvspwnL+3iFIMkF+JT7uJZ0RFYgq283p0t0bZbUoWsh66/aT++B8f7QcVdIrL4txhNOEGKKSvUfyZxZF3ye+suCemNtdZfPTf5RDU/1PdmGociriE1Q2BPJXAAyqZlErIDlF9TAR9QdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = string.Empty
                    }
                }
            },
            new Project
            {
                Id = Guid.Parse(ids[9]),
                ProjectId = Guid.Parse(groupIds[9]),
                ProjectName = names[9],
                ProjectGroupId = Guid.Parse(groupIds[9]),
                DongleId = string.Empty,
                InvoiceNo = string.Empty,
                ValidUntil = DateTime.UtcNow.AddYears(1),
                ProjectOptions = new ProjectOptions
                {
                    EmailOptions = new EmailOptions(),
                    LineOptions = new LineOptions
                    {
                        Enabled = true,
                        TokenProvider = (int)TokenProviders.NonCommercialServer,
                        NumberOfMessagesSentThisMonth = 0,
                        TargetLimitThisMonth = 0,
                        Token =
                            "XsHQB267fv3aQvspwnL+3iFIMkF+JT7uJZ0RFYgq283p0t0bZbUoWsh66/aT++B8f7QcVdIrL4txhNOEGKKSvUfyZxZF3ye+suCemNtdZfPTf5RDU/1PdmGociriE1Q2BPJXAAyqZlErIDlF9TAR9QdB04t89/1O/w1cDnyilFU="
                    },
                    TelegramOptions = new TelegramOptions
                    {
                        Enabled = true,
                        Token = "8195040424:AAH4yq8EB4yY09ffeTKWh4JFkWq_5dl85HM",
                    }
                }
            }
        ];
    }

    public static void Seed(DbContext dbContext, IEnumerable<Project> entities)
    {
        XAlarmCenterSeed.Seed(dbContext, entities, TableName);
    }

    public static async Task SeedAsync(DbContext dbContext, IEnumerable<Project> entities,
        CancellationToken cancellationToken)
    {
        await XAlarmCenterSeed.SeedAsync(dbContext, entities, TableName, cancellationToken);
    }
}