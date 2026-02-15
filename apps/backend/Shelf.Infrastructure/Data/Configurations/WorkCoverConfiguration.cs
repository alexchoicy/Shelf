using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkCoverConfiguration : IEntityTypeConfiguration<WorkCover>
{
    public void Configure(EntityTypeBuilder<WorkCover> builder)
    {
        builder.ToTable("WorkCovers");

        builder.HasKey(cover => cover.Id);

        builder.Property(cover => cover.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(cover => cover.Work)
            .WithMany(work => work.Covers)
            .HasForeignKey(cover => cover.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cover => cover.File)
            .WithMany(file => file.WorkCovers)
            .HasForeignKey(cover => cover.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
