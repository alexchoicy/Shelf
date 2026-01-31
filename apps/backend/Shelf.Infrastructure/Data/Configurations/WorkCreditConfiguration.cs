using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkCreditConfiguration : IEntityTypeConfiguration<WorkCredit>
{
    public void Configure(EntityTypeBuilder<WorkCredit> builder)
    {
        builder.HasOne(credit => credit.Work)
            .WithMany(work => work.WorkCredits)
            .HasForeignKey(credit => credit.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(credit => credit.Party)
            .WithMany(party => party.WorkCredits)
            .HasForeignKey(credit => credit.PartyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
