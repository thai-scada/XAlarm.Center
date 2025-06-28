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

        return
        [
            new Project
            {
                Id = Guid.Parse(ids[0]),
                ProjectId = Guid.Parse("df7bef34-adff-427f-bd35-4880932b9e95"),
                ProjectName = "xWeb - xView",
                ProjectGroupId = Guid.Parse("df7bef34-adff-427f-bd35-4880932b9e95"),
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
                ProjectId = Guid.Parse("f4f47526-fe67-4c3f-a36b-68668f009e72"),
                ProjectName = "xWeb - TEDA",
                ProjectGroupId = Guid.Parse("f4f47526-fe67-4c3f-a36b-68668f009e72"),
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
                ProjectId = Guid.Parse("e154ce10-6eab-47bd-b96f-bf058633674b"),
                ProjectName = "Aroon Roong Hitechnology - Hon Chuan",
                ProjectGroupId = Guid.Parse("e154ce10-6eab-47bd-b96f-bf058633674b"),
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
                ProjectId = Guid.Parse("d9635148-f283-4378-9649-dfec9989e83c"),
                ProjectName = "Aroon Roong Hitechnology - Faculty of Medicine, Khon Kaen University",
                ProjectGroupId = Guid.Parse("d9635148-f283-4378-9649-dfec9989e83c"),
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
                ProjectId = Guid.Parse("09bdc733-78d8-4399-92fd-2e5e772045ad"),
                ProjectName = "Flowmeters Thai - Sirat Expressway",
                ProjectGroupId = Guid.Parse("09bdc733-78d8-4399-92fd-2e5e772045ad"),
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
                ProjectId = Guid.Parse("0d9b45cd-ff8d-4c68-9598-55409471db88"),
                ProjectName = "Grand Digital - Surin Hospital",
                ProjectGroupId = Guid.Parse("0d9b45cd-ff8d-4c68-9598-55409471db88"),
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
                ProjectId = Guid.Parse("51ad54da-29b8-428b-89e1-f01908bc5b48"),
                ProjectName = "Test",
                ProjectGroupId = Guid.Parse("51ad54da-29b8-428b-89e1-f01908bc5b48"),
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
                ProjectId = Guid.Parse("e79e6381-f5f2-425b-86a7-2203035b4b41"),
                ProjectName = "Grand Digital - Ayutthaya Hospital",
                ProjectGroupId = Guid.Parse("e79e6381-f5f2-425b-86a7-2203035b4b41"),
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
                ProjectId = Guid.Parse("356a9988-e698-46f2-87ee-bf52bf2498ce"),
                ProjectName = "xWeb - IoT",
                ProjectGroupId = Guid.Parse("356a9988-e698-46f2-87ee-bf52bf2498ce"),
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