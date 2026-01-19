using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasOne(tag => tag.Source)
            .WithMany(source => source.Tags)
            .HasForeignKey(tag => tag.SourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}