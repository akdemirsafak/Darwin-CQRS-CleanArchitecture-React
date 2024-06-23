using Darwin.AuthServer.Models.Requests.Users;
using Darwin.AuthServer.Requests.Users;
using Darwin.AuthServer.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.AuthServer.Controllers;


[Route("[action]")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _userService.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _userService.GetByIdAsync(id));
    }
    [Authorize]
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserRequest request)
    {
        return CreateActionResult(await _userService.UpdateAsync(userId, request));
    }
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(string userId)
    {
        return CreateActionResult(await _userService.DeleteAsync(userId));
    }
    [Authorize]
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
    [Authorize]
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
