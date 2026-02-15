using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyAliasConfiguration : IEntityTypeConfiguration<PartyAlias>
{
    public void Configure(EntityTypeBuilder<PartyAlias> builder)
    {
        builder.ToTable("PartyAliases");

        builder.HasKey(pAlias => pAlias.Id);

        builder.Property(pAlias => pAlias.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(pAlias => pAlias.Party)
            .WithMany(party => party.Aliases)
            .HasForeignKey(pAlias => pAlias.PartyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
