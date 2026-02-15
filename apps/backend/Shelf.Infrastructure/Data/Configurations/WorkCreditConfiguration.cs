using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkCreditConfiguration : IEntityTypeConfiguration<WorkCredit>
{
    public void Configure(EntityTypeBuilder<WorkCredit> builder)
    {
        builder.ToTable("WorkCredits");

        builder.HasKey(credit => credit.Id);

        builder.Property(credit => credit.Id)
            .ValueGeneratedOnAdd();

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
