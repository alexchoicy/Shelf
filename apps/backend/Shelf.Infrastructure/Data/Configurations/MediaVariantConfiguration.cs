using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class MediaVariantConfiguration : IEntityTypeConfiguration<MediaVariant>
{
    public void Configure(EntityTypeBuilder<MediaVariant> builder)
    {
        builder.HasOne(variant => variant.File)
            .WithMany(file => file.MediaVariants)
            .HasForeignKey(variant => variant.FileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(variant => variant.MediaAsset)
            .WithMany(asset => asset.Variants)
            .HasForeignKey(variant => variant.MediaAssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
