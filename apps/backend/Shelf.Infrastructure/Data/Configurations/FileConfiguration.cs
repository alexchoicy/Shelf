using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<Core.Entity.File>
{
    public void Configure(EntityTypeBuilder<Core.Entity.File> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(file => file.Id);
    }
}
