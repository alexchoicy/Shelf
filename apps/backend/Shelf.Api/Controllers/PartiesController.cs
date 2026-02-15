using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.Services;
using Shelf.Api.DTO;
using System.Security.Claims;
using Shelf.Core.Exceptions;
using Shelf.Core.Models;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("parties")]
public class PartiesController(IPartyService partyService) : ControllerBase
{
    private readonly IPartyService _partyService = partyService;

    // thinking to merge into /parties endpoint? with pagination?
    [HttpGet("list")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PartyListModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PartyListModel>>> List()
    {
        var parties = await _partyService.GetAllForListAsync();

        var dto = parties.Select(p => new PartyListModel
        {
            PartyId = p.PartyId,
            PartyName = p.PartyName,
            PartyNormalizedName = p.PartyNormalizedName,
            PartyAliases = p.PartyAliases.Select(a => new PartyAliasModel
            {
                AliasName = a.AliasName,
                AliasNormalizedName = a.AliasNormalizedName
            }).ToList()
        }).ToList();

        return Ok(dto);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Create([FromBody] DTO.CreatePartyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        PartyCreationRequest coreReq = new()
        {
            Name = request.Name,
            PartyType = request.PartyType
        };


        await _partyService.CreatePartyAsync(coreReq, userId);

        return Created();
    }
}
