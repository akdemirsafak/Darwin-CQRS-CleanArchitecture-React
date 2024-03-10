using Darwin.Domain.BaseDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Presentation.Abstraction
{
    [ApiController]
    [Route("[controller]")]
    //[EnableRateLimiting("TokenBucket")]
    public class CustomBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public CustomBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [NonAction]
        public IActionResult CreateActionResult<T>(DarwinResponse<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
