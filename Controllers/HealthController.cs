using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiProject.Controllers;

[ApiController]
[Route("health")]
[Produces("application/json")]
[SwaggerTag("Health check operations")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Health status of the API</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Health check", Description = "Returns the health status of the API")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}

