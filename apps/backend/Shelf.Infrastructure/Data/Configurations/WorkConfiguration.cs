using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.HasOne(work => work.Uploader)
            .WithMany(user => user.UploadedWorks)
            .HasForeignKey(work => work.UploaderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
