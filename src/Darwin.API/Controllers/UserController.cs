using Darwin.Model.Request.Users;
using Darwin.Service.Features.Common;
using Darwin.Service.Features.Users;
using Darwin.Service.Features.Users.Commands;
using Darwin.Service.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;

[Authorize]
[Route("[action]")]
public class UserController : CustomBaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetUser.Query()));
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

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        return CreateActionResult(await _mediator.Send(new ConfirmEmail.Command(userId, token)));
    }

}
