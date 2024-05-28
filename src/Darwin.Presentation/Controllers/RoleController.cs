using Darwin.Application.Features.Roles;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;


public class RoleController : CustomBaseController
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
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
