using Darwin.Service.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class RoleController : CustomBaseController
{
    public RoleController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetRoles.Query()));
    }
    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        return CreateActionResult(await _mediator.Send(new CreateRole.Command(name)));
    }
}
