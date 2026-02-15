using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkSourceConfiguration : IEntityTypeConfiguration<WorkSource>
{
    public void Configure(EntityTypeBuilder<WorkSource> builder)
    {
        builder.ToTable("WorkSources");

        builder.HasKey(source => source.Id);

        builder.Property(source => source.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(source => source.Work)
            .WithMany(work => work.WorkSources)
            .HasForeignKey(source => source.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(source => source.Source)
            .WithMany(sourceEntity => sourceEntity.WorkSources)
            .HasForeignKey(source => source.SourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
