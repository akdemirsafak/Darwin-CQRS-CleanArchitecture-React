using Darwin.Model.Request.AgeRates;
using Darwin.Service.Features.AgeRates.Commands;
using Darwin.Service.Features.AgeRates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class AgeRateController : CustomBaseController
{
    public AgeRateController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetAgeRates.Query()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgeRateRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateAgeRate.Command(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAgeRateRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateAgeRate.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteAgeRate.Command(id)));
    }

}
