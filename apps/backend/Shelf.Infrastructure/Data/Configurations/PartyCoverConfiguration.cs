using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyCoverConfiguration : IEntityTypeConfiguration<PartyCover>
{
    public void Configure(EntityTypeBuilder<PartyCover> builder)
    {
        builder.ToTable("PartyCovers");

        builder.HasKey(cover => cover.Id);

        builder.Property(cover => cover.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(cover => cover.Party)
            .WithMany(party => party.Covers)
            .HasForeignKey(cover => cover.PartyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cover => cover.File)
            .WithMany(file => file.PartyCovers)
            .HasForeignKey(cover => cover.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
