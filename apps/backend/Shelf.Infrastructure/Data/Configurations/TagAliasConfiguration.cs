using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class TagAliasConfiguration : IEntityTypeConfiguration<TagAlias>
{
    public void Configure(EntityTypeBuilder<TagAlias> builder)
    {
        builder.HasOne(alias => alias.Tag)
            .WithMany(tag => tag.Aliases)
            .HasForeignKey(alias => alias.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}