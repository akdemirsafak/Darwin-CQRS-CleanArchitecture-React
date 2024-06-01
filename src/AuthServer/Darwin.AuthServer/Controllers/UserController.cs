using Darwin.AuthServer.Models.Requests.Users;
using Darwin.AuthServer.Requests.Users;
using Darwin.AuthServer.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.AuthServer.Controllers;

[Authorize]
[Route("[action]")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _userService.GetByIdAsync(id));
    }
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserRequest request)
    {
        return CreateActionResult(await _userService.UpdateAsync(userId, request));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string userId)
    {
        return CreateActionResult(await _userService.DeleteAsync(userId));
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        return CreateActionResult(await _userService.ChangePasswordAsync(request));
    }
    //[HttpPut]
    //public async Task<IActionResult> Suspend()
    //{
    //    return CreateActionResult(await _mediator.Send(new SuspendUser.Command()));
    //}
    [HttpGet]
    public async Task<IActionResult> SendConfirmationEmail(string userId)
    {
        return CreateActionResult(await _userService.SendConfirmationEmailAsync(userId));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        return CreateActionResult(await _userService.ConfirmEmailAsync(userId, token));
    }
}
