using Darwin.Model.Request.Users;
using Darwin.Service.Musics.Queries;
using Darwin.Service.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Darwin.API.Controllers;

public class UserController : CustomBaseController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateUserCommand(request)));
    }
}
