using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Options;
using XAlarm.Center.Domain.Settings;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.Configurations;

internal sealed class GlobalSettingConfiguration : EntityConfiguration<GlobalSetting>
{
    public override void Configure(EntityTypeBuilder<GlobalSetting> builder)
    {
        builder.ToTable("global_settings");

        builder.Property(x => x.LineOptions).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<LineOptions>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new LineOptions());

        builder.Property(x => x.TelegramOptions).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<TelegramOptions>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new TelegramOptions());

        builder.Property(x => x.EmailOptions).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<EmailOptions>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new EmailOptions());

        builder.Property(x => x.SmsOptions).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<SmsOptions>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new SmsOptions());

        base.Configure(builder);
    }
}