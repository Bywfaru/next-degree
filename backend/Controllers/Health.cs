using Microsoft.AspNetCore.Mvc;

namespace DegreePlanner.Controllers;

[Route("[controller]")]
[ApiController]
public class Health : ControllerBase
{
    [HttpGet("ping")]
    public ActionResult<string> GetPing()
    {
        return Ok("Pong");
    }
}
