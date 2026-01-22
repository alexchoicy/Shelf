using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class TagRelationshipConfiguration : IEntityTypeConfiguration<TagRelationship>
{
    public void Configure(EntityTypeBuilder<TagRelationship> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_TagRelationship_DifferentTags",
            "\"PrimaryTagId\" != \"RelatedTagId\""
        ));

        builder.HasOne(tr => tr.PrimaryTag)
            .WithMany(t => t.PrimaryRelationships)
            .HasForeignKey(tr => tr.PrimaryTagId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tr => tr.RelatedTag)
            .WithMany(t => t.RelatedRelationships)
            .HasForeignKey(tr => tr.RelatedTagId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tr => tr.CreatedByUser)
            .WithMany()
            .HasForeignKey(tr => tr.CreatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(tr => new { tr.PrimaryTagId, tr.RelatedTagId })
            .IsUnique();

        builder.HasIndex(tr => tr.RelationshipType);
    }
}
