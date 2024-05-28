using Darwin.Application.Features.RedisTests;
using Darwin.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Controllers;

public class RedisTestController : CustomBaseController
{
    private readonly IMediator _mediator;
    public RedisTestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTestCategoryData()
    {
        return CreateActionResult(await _mediator.Send(new RedisTest.RedisCategoryTestCommand()));
    }
}