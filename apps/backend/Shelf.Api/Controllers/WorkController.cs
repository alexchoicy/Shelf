using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.Services;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("works")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService) => _workService = workService;

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(DTO.WorkCreationResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<DTO.WorkCreationResponse>> Create([FromBody] DTO.WorkCreationRequest request)
    {
        string userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value!;

        Core.Models.WorkCreationRequest requestMapped = new()
        {
            Title = request.Title,
            Description = request.Description,
            Type = request.Type,
            Visibility = request.Visibility,
            Rating = request.Rating,
            CoverHash = request.CoverHash,
            IsAI = request.IsAI,
            NovelContent = request.NovelContent,
            PrimaryPartyId = request.PrimaryPartyId,
            ReleasedAt = request.ReleasedAt,
            MediaItems = request.MediaItems.Select(mi => new Core.Models.MediaItemCreationRequest
            {
                FileHash = mi.FileHash,
                Description = mi.Description,
                MediaType = mi.MediaType,
                Order = mi.Order,
                Kind = mi.Kind
            }).ToList()
        };


        Core.Models.WorkCreationResponse result = await _workService.CreateWorkAsync(requestMapped, userId);

        DTO.WorkCreationResponse response = new()
        {
            WorkId = result.WorkId,
            CoverUploadURL = result.CoverUploadURL,
            MediaItems = result.MediaItems.Select(mi => new DTO.MediaItemUploadInfo
            {
                MediaItemId = mi.MediaItemId,
                UploadURL = mi.UploadURL
            }).ToList()
        };

        return Ok(response);
    }
}
