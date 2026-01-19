using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyConfiguration : IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.HasOne(party => party.CoverFile)
            .WithMany(file => file.CoverParties)
            .HasForeignKey(party => party.CoverFileId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}