using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyCoverConfiguration : IEntityTypeConfiguration<PartyCover>
{
    public void Configure(EntityTypeBuilder<PartyCover> builder)
    {
        builder.HasOne(cover => cover.Party)
            .WithMany(party => party.Covers)
            .HasForeignKey(cover => cover.PartyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cover => cover.File)
            .WithMany(file => file.PartyCovers)
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
