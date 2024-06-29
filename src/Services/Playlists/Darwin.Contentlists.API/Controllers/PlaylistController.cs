using Darwin.Contentlists.Core.Dtos;
using Darwin.Contentlists.Core.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Contentlists.API.Controllers;


public class PlaylistController : CustomBaseController
{
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _playlistService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _playlistService.GetByIdAsync(id));
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistRequest request)
    {
        return CreateActionResult(await _playlistService.CreateAsync(request));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlaylistRequest request)
    {
        return CreateActionResult(await _playlistService.UpdateAsync(id, request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _playlistService.DeleteAsync(id));
    }
}
