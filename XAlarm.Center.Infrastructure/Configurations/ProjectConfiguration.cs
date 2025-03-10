using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Projects;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.Configurations;

internal sealed class ProjectConfiguration : EntityConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.Property(x => x.ProjectId);
        builder.Property(x => x.ProjectGroupId);
        builder.Property(x => x.ProjectName).HasMaxLength(255);;
        builder.Property(x => x.ValidUntil);
        builder.Property(x => x.DongleId).HasMaxLength(50);
        builder.Property(x => x.InvoiceNo).HasMaxLength(50);

        builder.Property(x => x.ProjectOptions).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<ProjectOptions>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new ProjectOptions());

        builder.HasIndex(x => x.ProjectId).IsUnique();

        base.Configure(builder);
    }
}