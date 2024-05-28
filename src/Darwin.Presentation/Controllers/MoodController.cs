using Darwin.Application.Features.Moods.Commands;
using Darwin.Application.Features.Moods.Queries;
using Darwin.Domain.RequestModels;
using Darwin.Domain.RequestModels.Moods;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;


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
        return CreateActionResult(await _mediator.Send(new GetMoods.Query()));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> List([FromBody] GetPaginationListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new GetMoodList.Query(request)));
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateMoodRequest request)
    {

        return CreateActionResult(await _mediator.Send(new CreateMood.Command(request)));
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMoodRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateMood.Command(id, request)));
    }
}
