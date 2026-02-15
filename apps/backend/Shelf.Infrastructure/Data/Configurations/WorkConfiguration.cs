using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Core.Entity;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.ToTable("Works");

        builder.HasKey(work => work.Id);

        builder.HasOne<User>()
            .WithMany(user => user.UploadedWorks)
            .HasForeignKey(work => work.UploaderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
