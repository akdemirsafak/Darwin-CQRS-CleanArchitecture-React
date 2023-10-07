using Darwin.Model.Request.Users;
using Darwin.Service.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darwin.API.Controllers;

public class UserController : CustomBaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateUser.Command(request)));
    }
}
