using Microsoft.EntityFrameworkCore;
using Shelf.Core.Exceptions;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Data;
using Shelf.Core.Entity;
using Shelf.Core.Utils;

namespace Shelf.Infrastructure.Services.Party;

public class PartyService(AppDbContext db) : IPartyService
{
    private readonly AppDbContext _db = db;

    public async Task<IReadOnlyList<PartyListModel>> GetAllForListAsync()
    {
        List<PartyListModel> result = await _db.Parties
            .AsNoTracking()
            .Select(p => new PartyListModel
            {
                PartyId = p.Id,
                PartyName = p.Name,
                PartyNormalizedName = p.NormalizedName,
                PartyAliases = p.Aliases
                    .Select(a => new PartyAliasModel
                    {
                        AliasName = a.Name,
                        AliasNormalizedName = a.NormalizedName
                    }).ToList()
            }).ToListAsync();

        return result;
    }

    public async Task<PartyCreationResponse> CreatePartyAsync(PartyCreationRequest request, string userId)
    {
        string normalized = StringUtils.NormalizeString(request.Name);
        // I think it should not check aliases here, because they are possible different party
        bool exists = await _db.Parties.AnyAsync(p => p.NormalizedName == normalized);
        if (exists) throw new DuplicateEntityException("Party or alias with the same name");

        Core.Entity.Party entity = new()
        {
            Name = request.Name,
            NormalizedName = normalized,
            Type = request.PartyType
        };

        _db.Parties.Add(entity);
        await _db.SaveChangesAsync();

        return new PartyCreationResponse
        {
            PartyId = entity.Id,
            PartyName = entity.Name,
            PartyType = entity.Type,
            CreatedAt = entity.CreatedAt
        };
    }

}
