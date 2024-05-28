using Darwin.Application.Features.Users;
using Darwin.Application.Features.Users.Commands;
using Darwin.Application.Features.Users.Queries;
using Darwin.Domain.RequestModels.Users;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;

[Authorize]
[Route("[action]")]
public class UserController : CustomBaseController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetUser.Query(id.ToString())));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateUser.Command(request)));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return CreateActionResult(await _mediator.Send(new DeleteUser.Command()));
    }
    [HttpPut]
    public async Task<IActionResult> Suspend()
    {
        return CreateActionResult(await _mediator.Send(new SuspendUser.Command()));
    }
    [HttpGet]
    public async Task<IActionResult> VerifyEmail()
    {
        return CreateActionResult(await _mediator.Send(new VerifyEmail.Command()));
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        return CreateActionResult(await _mediator.Send(new ConfirmEmail.Command(userId, token)));
    }
}
