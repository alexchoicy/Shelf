using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyAliasConfiguration : IEntityTypeConfiguration<PartyAlias>
{
    public void Configure(EntityTypeBuilder<PartyAlias> builder)
    {
        builder.HasOne(alias => alias.Party)
            .WithMany(party => party.Aliases)
            .HasForeignKey(alias => alias.PartyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}