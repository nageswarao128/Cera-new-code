using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TestCERAClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CeraController : ControllerBase
    {

        private readonly ILogger<CeraController> _logger;
        ICeraPlatform _ceraCloud;
        public CeraController(ILogger<CeraController> logger, ICeraPlatform ceraCloud)
        {
            _logger = logger;
            _ceraCloud = ceraCloud;
        }
        /// <summary>
        ///Based on the cloud this method will retrives subscription details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        /// <summary>
        /// This method will retrives the subscription data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
    }
}
