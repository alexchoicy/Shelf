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
            PrimaryPartyId = request.PrimaryPartyId,
            NovelContent = request.NovelContent,
            Medium = request.Medium,
            Type = request.Type,
            Visibility = request.Visibility,
            Rating = request.Rating,
            IsAI = request.IsAI,
            ReleasedAt = request.ReleasedAt,
            CoverHash = request.CoverHash,
            CoverMimeType = request.CoverMimeType,
            CoverWidth = request.CoverWidth,
            CoverHeight = request.CoverHeight,
            MediaItems = request.MediaItems.Select(mi => new Core.Models.MediaItemCreationRequest
            {
                FileHash = mi.FileHash,
                Description = mi.Description,
                MediaType = mi.MediaType,
                MimeType = mi.MimeType,
                FileSize = mi.FileSize,
                Order = mi.Order,
                Kind = mi.Kind,
                Width = mi.Width,
                Height = mi.Height
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
                FileHash = mi.FileHash,
                UploadURL = mi.UploadURL,
            }).ToList()
        };

        return Ok(response);
    }
}
