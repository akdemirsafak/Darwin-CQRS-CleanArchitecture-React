using Darwin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Shared.Base
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableRateLimiting("TokenBucket")]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CreateActionResult<T>(DarwinResponse<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
