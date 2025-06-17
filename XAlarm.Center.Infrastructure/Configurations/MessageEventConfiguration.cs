using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Events;
using XAlarm.Center.Domain.Messages;
using XAlarm.Center.Shared.Helpers;

namespace XAlarm.Center.Infrastructure.Configurations;

internal sealed class MessageEventConfiguration : EntityConfiguration<MessageEvent>
{
    public override void Configure(EntityTypeBuilder<MessageEvent> builder)
    {
        builder.ToTable("message_events");

        builder.Property(x => x.ProjectId);
        builder.Property(x => x.IsSuccess);
        builder.Property(x => x.EventBeginOnUtc);
        builder.Property(x => x.EventEndOnUtc);
        builder.Property(x => x.Type);
        builder.Property(x => x.TypeDescription).HasMaxLength(255);
        builder.Property(x => x.MessageBegin).HasColumnType("TEXT");
        builder.Property(x => x.MessageEnd).HasColumnType("TEXT");
        builder.Property(x => x.CreatedBy).HasMaxLength(255);

        builder.Property(x => x.AlarmPayload).HasColumnType("jsonb").HasConversion(
            x => JsonSerializer.Serialize(x, JsonHelper.DefaultJsonSerializerOptions),
            x => JsonSerializer.Deserialize<AlarmPayload>(x, JsonHelper.DefaultJsonSerializerOptions) ??
                 new AlarmPayload());

        builder.HasIndex(x => x.ProjectId).IsUnique();

        builder.Ignore(x => x.IsFailure);

        base.Configure(builder);
    }
}