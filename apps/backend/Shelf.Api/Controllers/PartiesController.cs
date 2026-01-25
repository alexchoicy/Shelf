using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.Services;
using Shelf.Api.DTO;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("parties")]
public class PartiesController : ControllerBase
{
    private readonly IPartyService _partyService;

    public PartiesController(IPartyService partyService) => _partyService = partyService;

    // thinking to merge into /parties endpoint? with pagination?
    [HttpGet("list")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PartyListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PartyListDto>>> List()
    {
        var parties = await _partyService.GetAllForListAsync();

        var dto = parties.Select(p => new PartyListDto
        {
            PartyId = p.PartyId,
            PartyName = p.PartyName,
            PartyNormalizedName = p.PartyNormalizedName,
            PartyAliases = p.PartyAliases.Select(a => new PartyAliasDto
            {
                AliasName = a.AliasName,
                AliasNormalizedName = a.AliasNormalizedName
            }).ToList()
        }).ToList();

        return Ok(dto);
    }
}