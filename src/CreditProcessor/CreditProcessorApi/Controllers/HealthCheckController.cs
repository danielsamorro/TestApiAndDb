using Microsoft.AspNetCore.Mvc;

namespace CreditProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet] public IActionResult Get()
        {
            return Ok();
        }
    }
}
