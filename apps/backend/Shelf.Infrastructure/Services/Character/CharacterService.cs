using Microsoft.EntityFrameworkCore;
using Shelf.Core.Entity;
using Shelf.Core.Exceptions;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Core.Utils;
using Shelf.Infrastructure.Data;

namespace Shelf.Infrastructure.Services.Character;

public class CharacterService(AppDbContext db) : ICharacterService
{
    private readonly AppDbContext _db = db;

    public async Task<IReadOnlyList<CharacterListModel>> GetAllForListAsync()
    {
        List<CharacterListModel> result = await _db.Characters
            .AsNoTracking()
            .Select(character => new CharacterListModel
            {
                CharacterId = character.Id,
                CharacterName = character.Name,
                CharacterNormalizedName = character.NormalizedName,
                SeriesId = character.SeriesId,
                CreatorPartyId = character.CreatorPartyId,
                CharacterAliases = character.Aliases
                    .Select(alias => new CharacterAliasModel
                    {
                        AliasName = alias.Name,
                        AliasNormalizedName = alias.NormalizedName
                    }).ToList()
            }).ToListAsync();

        return result;
    }

    public async Task CreateCharacterAsync(CharacterCreationRequest request, string userId)
    {
        bool seriesExists = await _db.Series.AnyAsync(series => series.Id == request.SeriesId);
        if (!seriesExists) throw new EntityNotFoundException("Series not found");

        if (request.CreatorPartyId is Guid creatorPartyId)
        {
            bool partyExists = await _db.Parties.AnyAsync(party => party.Id == creatorPartyId);
            if (!partyExists) throw new EntityNotFoundException("Creator party not found");
        }

        string normalizedName = StringUtils.NormalizeString(request.Name);

        bool exists = await _db.Characters.AnyAsync(character =>
            character.NormalizedName == normalizedName);

        if (exists) throw new DuplicateEntityException("Character", "Character with the same name already exists in this series");

        Core.Entity.Character entity = new()
        {
            Name = request.Name,
            SeriesId = request.SeriesId,
            CreatorPartyId = request.CreatorPartyId
        };

        _db.Characters.Add(entity);
        await _db.SaveChangesAsync();
    }
}
