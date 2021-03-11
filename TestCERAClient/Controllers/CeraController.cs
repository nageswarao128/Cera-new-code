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

        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
        [HttpGet]
        public IEnumerable<CeraResources> GetCloudResources(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudResourceList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraResources> GetDBResources()
        {
            return _ceraCloud.GetResourcesList();
        }
        [HttpGet]
        public IEnumerable<CeraVM> GetCloudVM(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudVMList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraVM> GetDBVM()
        {
            //var data= _ceraCloud.GetVMList(); 
            return _ceraCloud.GetVMList();
        }
    }
}
