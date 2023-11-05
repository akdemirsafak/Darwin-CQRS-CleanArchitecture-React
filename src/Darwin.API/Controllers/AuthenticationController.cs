using Darwin.Model.Request.Authentications;
using Darwin.Service.Features.Authentications;
using Darwin.Service.Features.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class AuthenticationController : CustomBaseController
{
    public AuthenticationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return CreateActionResult(await _mediator.Send(new Login.Command(request)));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return CreateActionResult(await _mediator.Send(new Register.Command(request)));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        return CreateActionResult(await _mediator.Send(new ConfirmEmail.Command(userId, token)));
    }
}
