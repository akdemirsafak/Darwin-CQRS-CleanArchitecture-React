using Darwin.Application.Features.Youtube;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;


public class YoutubeController : CustomBaseController
{
    private readonly IMediator _mediator;
    public YoutubeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> Get(string? pageToken, int maxResults = 10)
    {

        return CreateActionResult(await _mediator.Send(new GetTheWeekndChannel.Command(pageToken, maxResults)));
    }
}
