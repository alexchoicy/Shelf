using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class CharacterAliasConfiguration : IEntityTypeConfiguration<CharacterAlias>
{
    public void Configure(EntityTypeBuilder<CharacterAlias> builder)
    {
        builder.ToTable("CharacterAliases");

        builder.HasKey(character => character.Id);

        builder.Property(character => character.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(cAlias => cAlias.Character)
            .WithMany(character => character.Aliases)
            .HasForeignKey(cAlias => cAlias.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
