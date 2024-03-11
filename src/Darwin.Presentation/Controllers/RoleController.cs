using Darwin.Application.Features.Roles;
using Darwin.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;


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
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        return CreateActionResult(await _mediator.Send(new CreateRole.Command(name)));
    }
}
