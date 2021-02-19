using CERA.CloudService;
using CERA.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TestCERAClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {

        IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        ICeraCloudApiService _ceraCloud;

        public HomeController(ILogger<HomeController> logger, ICeraCloudApiService ceraCloud, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _ceraCloud = ceraCloud;
            _ceraCloud.Initialize(_config.GetSection("AzureConfig:TenentID").Value, _config.GetSection("AzureConfig:ClientID").Value, _config.GetSection("AzureConfig:ClientSecret").Value);
        }

        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions()
        {
            return _ceraCloud.GetCloudSubscriptionList();
        }
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
    }
}
