using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Infrastructure.Configurations;

internal class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
    }
}