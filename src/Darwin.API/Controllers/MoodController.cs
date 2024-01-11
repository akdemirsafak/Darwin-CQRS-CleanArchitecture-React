using Darwin.Model.Request;
using Darwin.Model.Request.Moods;
using Darwin.Service.Features.Moods.Commands;
using Darwin.Service.Features.Moods.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class MoodController : CustomBaseController
{
    public MoodController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetMoods.Query()));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> GetList([FromBody] GetPaginationListRequest request)
    {
        return CreateActionResult(await _mediator.Send(new GetMoodList.Query(request)));
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMoodRequest request)
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
