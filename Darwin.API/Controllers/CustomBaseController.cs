using Darwin.Core.BaseDto;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
