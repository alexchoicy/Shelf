using Microsoft.EntityFrameworkCore;
using Shelf.Core.Enum;
using Shelf.Core.Entity;
using Shelf.Core.Utils;

namespace Shelf.Infrastructure.Data.Seed;

public class PartySeed
{
    public static async Task SeedAsync(DbContext context)
    {
        Party unknownParty = new()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Unknown",
            Type = PartyType.Individual,
        };

        if (!await context.Set<Party>().AnyAsync(p => p.Id == unknownParty.Id))
        {
            await context.Set<Party>().AddAsync(unknownParty);
        }

        List<Party> partiesToNormalize = await context.Set<Party>()
            .Where(p => string.IsNullOrWhiteSpace(p.NormalizedName))
            .ToListAsync();

        foreach (Party party in partiesToNormalize)
        {
            party.NormalizedName = StringUtils.NormalizeString(party.Name);
        }

        if (partiesToNormalize.Count > 0 || context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}
