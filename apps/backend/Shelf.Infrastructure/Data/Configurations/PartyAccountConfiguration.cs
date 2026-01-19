using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class PartyAccountConfiguration : IEntityTypeConfiguration<PartyAccount>
{
    public void Configure(EntityTypeBuilder<PartyAccount> builder)
    {
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