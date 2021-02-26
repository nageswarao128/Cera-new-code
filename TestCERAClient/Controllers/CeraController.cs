using CERA.CloudService;
using CERA.Converter;
using CERA.DataOperation.Data;
using CERA.Entities;
using CERA.Entities.ViewModel;
using CERAAPI.Data;
using CERAAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestCERAClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CeraController : ControllerBase
    {

        IConfiguration _config;
        private readonly ILogger<CeraController> _logger;
        ICeraCloudApiService _ceraCloud;
        ICeraConverter _ceraConverter;
        CeraAPIUserDbContext _userDb;
        public CeraController(ILogger<CeraController> logger, CeraAPIUserDbContext userDb, ICeraCloudApiService ceraCloud, IConfiguration config, ICeraConverter ceraConverter)
        {
            _config = config;
            _logger = logger;
            _ceraCloud = ceraCloud;
            _ceraConverter = ceraConverter;
            _userDb = userDb;
            //_ceraCloud.Initialize(_config.GetSection("AzureConfig:TenentID").Value, _config.GetSection("AzureConfig:ClientID").Value, _config.GetSection("AzureConfig:ClientSecret").Value);
        }

        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions()
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = from clientPlugin in _userDb.ClientCloudPlugins
                       where clientPlugin.Client.ClientName == "Quadrant"
                       join cloudPlugin in _userDb.CloudPlugIns
                       on clientPlugin.PlugIn.Id equals cloudPlugin.Id
                       select new CeraPlatformConfig
                       {
                           PlatformName = clientPlugin.PlugIn.CloudProviderName,
                           APIClassName = clientPlugin.PlugIn.FullyQualifiedClassName,
                           DllPath = clientPlugin.PlugIn.DllPath,
                       };
            _ceraCloud._platformConfigs = data.ToList();
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
    }
}
