using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("Sources");

        builder.HasKey(source => source.Id);

        builder.Property(source => source.Id)
            .ValueGeneratedOnAdd();
    }
}
