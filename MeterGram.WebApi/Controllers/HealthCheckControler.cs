using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers;

[ApiVersionNeutral]
[Route("api/health-check")]
[ApiController]
public class HealthCheckControler : ControllerBase
{
    [HttpGet]
    [Route("ping")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public static IActionResult Ping()
    {
        return new NoContentResult();
    }
}
