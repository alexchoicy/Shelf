using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;
using Shelf.Core.Entity;

namespace Shelf.Infrastructure.Data.Seed;

public class SeriesSeed
{
    public static async Task SeedAsync(DbContext context)
    {
        Series unknownSeries = new()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Original",
        };

        if (!await context.Set<Series>().AnyAsync(p => p.Id == unknownSeries.Id))
        {
            await context.Set<Series>().AddAsync(unknownSeries);
            await context.SaveChangesAsync();
        }
    }
}
