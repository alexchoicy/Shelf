using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkTagConfiguration : IEntityTypeConfiguration<WorkTag>
{
    public void Configure(EntityTypeBuilder<WorkTag> builder)
    {
        builder.HasOne(tag => tag.Work)
            .WithMany(work => work.WorkTags)
            .HasForeignKey(tag => tag.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tag => tag.Tag)
            .WithMany(entityTag => entityTag.WorkTags)
            .HasForeignKey(tag => tag.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tag => tag.AssertedByWorkSource)
            .WithMany(workSource => workSource.AssertedWorkTags)
            .HasForeignKey(tag => tag.AssertedByWorkSourceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(tag => tag.AssertedByUser)
            .WithMany(user => user.AssertedWorkTags)
            .HasForeignKey(tag => tag.AssertedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(tag => tag.AssertedByModel)
            .WithMany(model => model.AssertedWorkTags)
            .HasForeignKey(tag => tag.AssertedByModelId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}