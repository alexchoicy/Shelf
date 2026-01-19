using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class MediaItemConfiguration : IEntityTypeConfiguration<MediaItem>
{
    public void Configure(EntityTypeBuilder<MediaItem> builder)
    {
        builder.HasOne(item => item.File)
            .WithMany(file => file.MediaItems)
            .HasForeignKey(item => item.FileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(item => item.Work)
            .WithMany(work => work.MediaItems)
            .HasForeignKey(item => item.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}