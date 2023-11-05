using Darwin.Core.Entities;
using Darwin.Model.Request.Authentications;
using Darwin.Service.Features.Authentications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers;


public class AuthenticationController : CustomBaseController
{
    private readonly UserManager<AppUser> _userManager;

    public AuthenticationController(IMediator mediator, UserManager<AppUser> userManager) : base(mediator)
    {
        _userManager = userManager;
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
    [HttpPost("[action]")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var existUser= await _userManager.FindByIdAsync(userId);
        await _userManager.ConfirmEmailAsync(existUser, token);
        return Ok();
    }
}
