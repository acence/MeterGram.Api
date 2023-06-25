using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers;

[ApiVersionNeutral]
[Route("api/health-check")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [Route("ping")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult Ping()
    {
        return new NoContentResult();
    }
}
