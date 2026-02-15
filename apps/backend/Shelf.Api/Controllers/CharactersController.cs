using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.Models;
using Shelf.Core.Services;
using System.Security.Claims;

namespace Shelf.Api.Controllers;

[ApiController]
[Authorize]
[Route("characters")]
public class CharactersController(ICharacterService characterService) : ControllerBase
{
    private readonly ICharacterService _characterService = characterService;

    [HttpGet("list")]
    [ProducesResponseType(typeof(IEnumerable<CharacterListModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CharacterListModel>>> List()
    {
        var characters = await _characterService.GetAllForListAsync();

        var dto = characters.Select(character => new CharacterListModel
        {
            CharacterId = character.CharacterId,
            CharacterName = character.CharacterName,
            CharacterNormalizedName = character.CharacterNormalizedName,
            SeriesId = character.SeriesId,
            CreatorPartyId = character.CreatorPartyId,
            CharacterAliases = character.CharacterAliases.Select(alias => new CharacterAliasModel
            {
                AliasName = alias.AliasName,
                AliasNormalizedName = alias.AliasNormalizedName
            }).ToList()
        }).ToList();

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Create([FromBody] DTO.CreateCharacterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        CharacterCreationRequest coreReq = new()
        {
            Name = request.Name,
            SeriesId = request.SeriesId,
            CreatorPartyId = request.CreatorPartyId
        };

        await _characterService.CreateCharacterAsync(coreReq, userId);

        return Created();
    }
}
