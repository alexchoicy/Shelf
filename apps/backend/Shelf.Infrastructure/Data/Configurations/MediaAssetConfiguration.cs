using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        builder.HasOne(asset => asset.Work)
            .WithMany(work => work.MediaAssets)
            .HasForeignKey(asset => asset.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}