using System;
using Microsoft.AspNetCore.Mvc;
using Shelf.Core.StorageServices;
using Microsoft.AspNetCore.Http;

namespace Shelf.Api.Controllers;

[ApiController]
[Route("local")]
public class LocalController : ControllerBase
{
    private readonly ILocalStorageService _localStorageService;

    public LocalController(ILocalStorageService localStorageService) => _localStorageService = localStorageService;

    [HttpPost("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromRoute] Guid id, [FromForm] IFormFile? file)
    {
        if (file is null)
            return BadRequest(new { message = "File is required." });

        await _localStorageService.MarkAsync(id, file.FileName, file.Length);

        return Ok(new { id, fileName = file.FileName, size = file.Length });
    }
}
