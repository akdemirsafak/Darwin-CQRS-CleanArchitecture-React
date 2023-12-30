using Darwin.Model.Request.PlayLists;
using Darwin.Service.Features.PlayLists.Commands;
using Darwin.Service.Features.PlayLists.Queries;
using Darwin.Service.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;

[Authorize]
public class PlayListController : CustomBaseController
{
    private readonly ICurrentUser _currentUser;
    public PlayListController(IMediator mediator, ICurrentUser currentUser) : base(mediator)
    {
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetPlayLists.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetPlayListById.Query(id)));
    }
    [HttpGet("GetMyPlayLists")]
    public async Task<IActionResult> GetMyPlayLists()
    {
        return CreateActionResult(await _mediator.Send(new GetMyPlayLists.Query(_currentUser.GetUserId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreatePlayList.Command(request, _currentUser.GetUserId)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdatePlayListRequest request, Guid id)
    {
        return CreateActionResult(await _mediator.Send(new UpdatePlayList.Command(id, request, _currentUser.GetUserId)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeletePlayList.Command(id, _currentUser.GetUserId)));
    }
    [HttpPost("AddContentsToPlayList")]
    public async Task<IActionResult> AddContentToPlayList([FromBody] AddContentsToPlayListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new AddContentsToPlayList.Command(request, _currentUser.GetUserId)));
    }
    [HttpPost("RemoveContentsFromPlayList")]
    public async Task<IActionResult> RemoveContentsFromPlayList([FromBody] RemoveContentsFromPlayListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new RemoveContentsFromPlayList.Command(request, _currentUser.GetUserId)));
    }

}
