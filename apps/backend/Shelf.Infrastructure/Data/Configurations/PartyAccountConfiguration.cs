using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyAccountConfiguration : IEntityTypeConfiguration<PartyAccount>
{
    public void Configure(EntityTypeBuilder<PartyAccount> builder)
    {
        builder.ToTable("PartyAccounts");

        builder.HasKey(account => account.Id);

        builder.Property(account => account.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(account => account.Party)
            .WithMany(party => party.Accounts)
            .HasForeignKey(account => account.PartyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(account => account.Source)
            .WithMany(source => source.PartyAccounts)
            .HasForeignKey(account => account.SourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
