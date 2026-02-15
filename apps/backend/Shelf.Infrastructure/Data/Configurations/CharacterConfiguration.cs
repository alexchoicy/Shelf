using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Characters");

        builder.HasKey(character => character.Id);

        builder.HasOne(character => character.CreatorParty)
            .WithMany(party => party.CreatedCharacters)
            .HasForeignKey(character => character.CreatorPartyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(character => character.Series)
            .WithMany(series => series.Characters)
            .HasForeignKey(character => character.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
