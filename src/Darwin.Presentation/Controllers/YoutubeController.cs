using Darwin.Application.Features.Youtube;
using Darwin.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;


public class YoutubeController : CustomBaseController
{
    public YoutubeController(IMediator mediator) : base(mediator)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get(string? pageToken, int maxResults = 10)
    {

        return CreateActionResult(await _mediator.Send(new GetTheWeekndChannel.Command(pageToken, maxResults)));
    }
}
