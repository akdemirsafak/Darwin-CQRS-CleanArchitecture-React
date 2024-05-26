using Microsoft.AspNetCore.Mvc;

namespace Darwin.Notification.Controllers;

public class NotificationController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Notification Service is running");
    }
}
