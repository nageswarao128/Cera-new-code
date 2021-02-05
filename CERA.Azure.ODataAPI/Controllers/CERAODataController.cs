using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CERA.Azure.ODataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CERAODataController : ControllerBase
    {


        private readonly ILogger<CERAODataController> _logger;

        public CERAODataController(ILogger<CERAODataController> logger)
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
