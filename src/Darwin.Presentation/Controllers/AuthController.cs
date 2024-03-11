using Darwin.Application.Features.Auth.Login;
using Darwin.Application.Features.Auth.RefreshToken;
using Darwin.Application.Features.Auth.Register;
using Darwin.Application.Features.Auth.RevokeAllTokens;
using Darwin.Application.Features.Auth.RevokeToken;
using Darwin.Application.Features.Users;
using Darwin.Domain.RequestModels.Authentications;
using Darwin.Domain.RequestModels.Users;
using Darwin.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AuthController : CustomBaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return CreateActionResult(await _mediator.Send(new Login.Command(request)));
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return CreateActionResult(await _mediator.Send(new Register.Command(request)));
    }


    //
    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email)
    {
        return CreateActionResult(await _mediator.Send(new ResetPasswordEmail.Command(email)));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordVerify([FromBody] ResetPasswordRequest request, string userId, string token)
    {
        return CreateActionResult(await _mediator.Send(new ResetPasswordEmailVerify.Command(request, userId, token)));
    }


    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request) //CreateTokenByRefreshToken
    {
        return CreateActionResult(await _mediator.Send(new CreateTokenByRefreshToken.Command(request)));
    }

    [HttpPost]
    public async Task<IActionResult> RevokeToken(string email)
    {
        return CreateActionResult(await _mediator.Send(new RevokeToken.Command(email)));
    }
    [HttpPost]
    public async Task<IActionResult> RevokeAllTokens()
    {
        return CreateActionResult(await _mediator.Send(new RevokeAllTokens.Command()));
    }
}
