using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkCoverConfiguration : IEntityTypeConfiguration<WorkCover>
{
    public void Configure(EntityTypeBuilder<WorkCover> builder)
    {
        builder.HasOne(cover => cover.Work)
            .WithMany(work => work.Covers)
            .HasForeignKey(cover => cover.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cover => cover.File)
            .WithMany(file => file.WorkCovers)
            .HasForeignKey(cover => cover.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cover => cover.SetByUser)
            .WithMany()
            .HasForeignKey(cover => cover.SetByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(cover => cover.SetBySource)
            .WithMany()
            .HasForeignKey(cover => cover.SetBySourceId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
