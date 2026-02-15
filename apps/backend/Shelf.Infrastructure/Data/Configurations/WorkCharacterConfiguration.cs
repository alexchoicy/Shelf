using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkCharacterConfiguration : IEntityTypeConfiguration<WorkCharacter>
{
    public void Configure(EntityTypeBuilder<WorkCharacter> builder)
    {
        builder.ToTable("WorkCharacters");

        builder.HasKey(wc => wc.Id);

        builder.Property(wc => wc.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(wc => wc.Work)
            .WithMany(w => w.WorkCharacters)
            .HasForeignKey(wc => wc.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wc => wc.Character)
            .WithMany(c => c.WorkCharacters)
            .HasForeignKey(wc => wc.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
