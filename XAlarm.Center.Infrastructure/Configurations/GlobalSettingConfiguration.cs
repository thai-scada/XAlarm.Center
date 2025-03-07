using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Settings;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.Configurations;

internal sealed class GlobalSettingConfiguration : EntityConfiguration<GlobalSetting>
{
    public override void Configure(EntityTypeBuilder<GlobalSetting> builder)
    {
        builder.ToTable("global_settings");

        builder.Property(x => x.GeneralSetting).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<GeneralSetting>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new GeneralSetting());

        base.Configure(builder);
    }
}