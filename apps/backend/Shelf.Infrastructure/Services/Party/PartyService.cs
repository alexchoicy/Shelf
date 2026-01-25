using Microsoft.EntityFrameworkCore;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Data;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Services.Party;

public class PartyService : IPartyService
{
    private readonly AppDbContext _db;

    public PartyService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<PartyListModel>> GetAllForListAsync()
    {
        var result = await _db.Parties
            .AsNoTracking()
            .Where(p => p.DeletedAt == null)
            .Select(p => new PartyListModel
            {
                PartyId = p.Id,
                PartyName = p.Name,
                PartyNormalizedName = p.NormalizedName,
                PartyAliases = p.Aliases
                    .Where(a => a.DeletedAt == null)
                    .Select(a => new PartyAliasModel
                    {
                        AliasName = a.Name,
                        AliasNormalizedName = a.NormalizedName
                    }).ToList()
            }).ToListAsync();

        return result;
    }
}