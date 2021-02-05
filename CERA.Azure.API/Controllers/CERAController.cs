using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CERA.Azure.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CERAController : ControllerBase
    {
        private readonly ILogger<CERAController> _logger;

        public CERAController(ILogger<CERAController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
