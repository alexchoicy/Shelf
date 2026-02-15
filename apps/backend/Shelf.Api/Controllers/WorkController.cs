using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.Models;
using Shelf.Core.Services;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("works")]
[Authorize]
public class WorkController(IWorkService workService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(WorkCreationResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<WorkCreationResponse>> Create([FromBody] DTO.WorkCreationRequest request)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        WorkCreationRequest requestMapped = new()
        {
            Title = request.Title,
            Description = request.Description,
            Credits = request.Credits.Select(c => new WorkCreationCredit
            {
                PartyId = c.PartyId,
                Role = c.Role
            }).ToList(),
            NovelContent = request.NovelContent,
            Medium = request.Medium,
            CharacterIds = request.CharacterIds,
            Type = request.Type,
            Visibility = request.Visibility,
            Rating = request.Rating,
            IsAI = request.IsAI,
            ReleasedAt = request.ReleasedAt,
            MediaItems = request.MediaItems.Select(mi => new MediaItemCreationRequest
            {
                SimpleBlake3 = mi.SimpleBlake3,
                Description = mi.Description,
                MediaType = mi.MediaType,
                TextContent = mi.TextContent,
                MimeType = mi.MimeType,
                FileSize = mi.FileSize,
                OriginalFileName = mi.OriginalFileName,
                Order = mi.Order,
                Kind = mi.Kind,
            }).ToList()
        };

        WorkCreationResponse response = await workService.CreateWorkAsync(requestMapped, userId, HttpContext.RequestAborted);

        return Ok(response);
    }
}
