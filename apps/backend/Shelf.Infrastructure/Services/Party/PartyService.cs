using Microsoft.EntityFrameworkCore;
using Shelf.Core.Exceptions;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Data;
using Shelf.Infrastructure.Entity;

namespace Shelf.Infrastructure.Services.Party;

public class PartyService(AppDbContext db) : IPartyService
{
    private readonly AppDbContext _db = db;

    public async Task<IReadOnlyList<PartyListModel>> GetAllForListAsync()
    {
        List<PartyListModel> result = await _db.Parties
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

    public async Task<PartyCreationResponse> CreatePartyAsync(PartyCreationRequest request, string userId)
    {
        string normalized = Extensions.StringExtensions.Normalize(request.Name);
        // I think it should not check aliases here, because they are possible different party
        bool exists = await _db.Parties.AnyAsync(p => p.NormalizedName == normalized);
        if (exists) throw new DuplicateEntityException("Party or alias with the same name");

        Entity.Party entity = new()
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

    public async Task<PartyDetailModel?> GetByIdAsync(Guid id)
    {
        PartyDetailModel? p = await _db.Parties.AsNoTracking().Where(x => x.Id == id && x.DeletedAt == null).Select(x => new PartyDetailModel
        {
            PartyId = x.Id,
            PartyName = x.Name,
            PartyType = x.Type,
            CreatedAt = x.CreatedAt
        }).FirstOrDefaultAsync();

        return p;
    }
}
