using Darwin.Service.ExternalService.YoutubeApi.GetTheWeekend;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class YoutubeController : CustomBaseController
{
    public YoutubeController(IMediator mediator) : base(mediator)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get(string? pageToken, int maxResults = 10)
    {

        return CreateActionResult(await _mediator.Send(new GetTheWeekendChannel.Command(pageToken, maxResults)));
    }
}
