using Microsoft.EntityFrameworkCore;
using Shelf.Infrastructure.Entity;
using Shelf.Core.Enum;

namespace Shelf.Infrastructure.Data.Seed;

public class PartySeed
{
    public static async Task SeedAsync(DbContext context)
    {
        Party unknownParty = new Party
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Unknown",
            NormalizedName = "Unknown",
            Type = PartyType.INDIVIDUAL,
        };

        if (!await context.Set<Party>().AnyAsync(p => p.Id == unknownParty.Id))
        {
            await context.Set<Party>().AddAsync(unknownParty);
            await context.SaveChangesAsync();
        }
    }
}