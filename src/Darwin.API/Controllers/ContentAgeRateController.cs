using Darwin.Model.Request.ContentAgeRates;
using Darwin.Service.Features.AgeRates.Commands;
using Darwin.Service.Features.AgeRates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class ContentAgeRateController : CustomBaseController
{
    public ContentAgeRateController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetContentAgeRates.Query()));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContentAgeRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateContentAgeRate.Command(request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteContentAgeRate.Command(id)));
    }

}
