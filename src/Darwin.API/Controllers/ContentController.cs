using Darwin.Model.Request.Contents;
using Darwin.Service.Features.Contents.Commands;
using Darwin.Service.Features.Contents.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;

[Authorize]
public class ContentController : CustomBaseController
{
    public ContentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetContents.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetContentById.Query(id)));
    }
    [HttpGet("Search")]
    public async Task<IActionResult> Search([FromQuery] string searchText)
    {
        return CreateActionResult(await _mediator.Send(new SearchContents.Query(searchText)));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContentRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateContent.Command(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContentRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateContent.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteContent.Command(id)));
    }
}
