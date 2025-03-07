using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.Configurations;

internal class MasterEntityConfiguration<TMasterEntity> : IEntityTypeConfiguration<TMasterEntity>
    where TMasterEntity : MasterEntity
{
    public virtual void Configure(EntityTypeBuilder<TMasterEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.NameAlt).HasMaxLength(255);
        builder.Property(x => x.NameAbbr).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Enable);
        builder.Property(x => x.SortOrder);
        builder.Property(x => x.Code);
        builder.Property(x => x.Key).HasMaxLength(100);
        builder.Property(x => x.Type);
        builder.Property(x => x.Status);
        builder.Property(x => x.Tags);
        builder.Property(x => x.IsDefault);
        builder.Property(x => x.CreatedOnUtc);
        builder.Property(x => x.CreatedUserId);
        builder.Property(x => x.UpdatedOnUtc);
        builder.Property(x => x.UpdatedUserId);
    }
}