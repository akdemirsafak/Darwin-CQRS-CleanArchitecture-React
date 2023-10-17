
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Darwin.Model.Request.AgeRates;
using Darwin.Service.Features.ContentAgeRates.Commands;
using Darwin.Service.Features.Moods.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;

//[Authorize]
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
