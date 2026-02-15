using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        builder.ToTable("MediaAssets");

        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(asset => asset.Work)
            .WithMany(work => work.MediaAssets)
            .HasForeignKey(asset => asset.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
