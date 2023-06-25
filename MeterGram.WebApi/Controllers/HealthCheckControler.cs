using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers;

/// <summary>
/// Rest API controller for health-check purposes
/// </summary>
[ApiVersionNeutral]
[Route("api/health-check")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Used if needed from external service to check availability
    /// </summary>
    [HttpGet]
    [Route("ping")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult Ping()
    {
        return new NoContentResult();
    }
}
