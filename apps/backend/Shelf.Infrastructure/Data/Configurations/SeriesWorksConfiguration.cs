using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Data.Configurations;

public class SeriesWorksConfiguration : IEntityTypeConfiguration<SeriesWorks>
{
    public void Configure(EntityTypeBuilder<SeriesWorks> builder)
    {
        builder.HasOne(seriesWork => seriesWork.Series)
            .WithMany(series => series.SeriesWorks)
            .HasForeignKey(seriesWork => seriesWork.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(seriesWork => seriesWork.Work)
            .WithMany(work => work.SeriesWorks)
            .HasForeignKey(seriesWork => seriesWork.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
