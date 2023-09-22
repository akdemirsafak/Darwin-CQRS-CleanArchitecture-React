using Darwin.Model.Request.Moods;
using Darwin.Service.Moods.Commands.Create;
using Darwin.Service.Moods.Commands.Update;
using Darwin.Service.Moods.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class MoodController : CustomBaseController
{
    private readonly IMediator _mediator;

    public MoodController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetMoodsQuery()));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMoodRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateMoodCommand(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMoodRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateMoodCommand(id, request)));
    }
}
