using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.Configurations;

internal class DataMapEntityConfiguration<TDataMapEntity> : IEntityTypeConfiguration<TDataMapEntity>
    where TDataMapEntity : DataMapEntity
{
    public virtual void Configure(EntityTypeBuilder<TDataMapEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Enable);
        builder.Property(x => x.SortOrder);
    }
}