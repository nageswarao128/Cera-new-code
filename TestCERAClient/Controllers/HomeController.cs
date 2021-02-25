using CERA.CloudService;
using CERA.Converter;
using CERA.DataOperation.Data;
using CERA.Entities;
using CERA.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestCERAClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {

        IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        ICeraCloudApiService _ceraCloud;
        ICeraConverter _ceraConverter;
        public HomeController(ILogger<HomeController> logger, ICeraCloudApiService ceraCloud, IConfiguration config, ICeraConverter ceraConverter)
        {
            _config = config;
            _logger = logger;
            _ceraCloud = ceraCloud;
            _ceraConverter = ceraConverter;
            //_ceraCloud.Initialize(_config.GetSection("AzureConfig:TenentID").Value, _config.GetSection("AzureConfig:ClientID").Value, _config.GetSection("AzureConfig:ClientSecret").Value);
        }

        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions()
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
    }
}
