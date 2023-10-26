using Darwin.Model.Request.PlayLists;
using Darwin.Service.Features.PlayLists.Commands;
using Darwin.Service.Features.PlayLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;

public class PlayListController : CustomBaseController
{
    public PlayListController(IMediator mediator) : base(mediator)
    {
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreatePlayList.Command(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdatePlayListRequest request, Guid id)
    {
        return CreateActionResult(await _mediator.Send(new UpdatePlayList.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeletePlayList.Command(id)));
    }
    [HttpPost("AddMusicToPlayList")]
    public async Task<IActionResult> AddMusicToPlayList([FromBody] AddMusicToPlayListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new AddMusicToPlayList.Command(request)));
    }

}
