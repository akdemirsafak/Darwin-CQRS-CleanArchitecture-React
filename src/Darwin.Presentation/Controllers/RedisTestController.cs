using Darwin.Application.Features.RedisTests;
using Darwin.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;

public class RedisTestController : CustomBaseController
{
    public RedisTestController(IMediator mediator) : base(mediator)
    {

    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTestCategoryData()
    {
        return CreateActionResult(await _mediator.Send(new RedisTest.RedisCategoryTestCommand()));
    }
}