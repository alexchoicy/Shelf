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

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DTO.CreatePartyResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<DTO.CreatePartyResponse>> GetById(Guid id)
    {
        var result = await _partyService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(new DTO.CreatePartyResponse
        {
            PartyId = result.PartyId,
            PartyName = result.PartyName,
            PartyType = result.PartyType,
            CreatedAt = result.CreatedAt
        });
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(CreatePartyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DTO.CreatePartyResponse>> Create([FromBody] DTO.CreatePartyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        PartyCreationRequest coreReq = new()
        {
            Name = request.Name,
            PartyType = request.PartyType
        };


        PartyCreationResponse result = await _partyService.CreatePartyAsync(coreReq, userId);

        CreatePartyResponse response = new()
        {
            PartyId = result.PartyId,
            PartyName = result.PartyName,
            PartyType = result.PartyType,
            CreatedAt = result.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = response.PartyId }, response);
    }
}
