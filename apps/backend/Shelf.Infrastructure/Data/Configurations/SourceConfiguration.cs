using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.HasMany(source => source.PartyAccounts)
            .WithOne(account => account.Source)
            .HasForeignKey(account => account.SourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(source => source.Tags)
            .WithOne(tag => tag.Source)
            .HasForeignKey(tag => tag.SourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(source => source.WorkSources)
            .WithOne(workSource => workSource.Source)
            .HasForeignKey(workSource => workSource.SourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}