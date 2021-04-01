using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        //[Authorize]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        [HttpGet]
        public async Task<List<CeraResourceHealth>> GetCloudResourceHealth(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return await _ceraCloud.GetCloudResourceHealth(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives Resources details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Resources data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraResources> GetCloudResources(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudResourceList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives Virtual Machines details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Virtual Machines data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraVM> GetCloudVM(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudVMList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives ResourceGroups details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of ResourceGroups data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraResourceGroups> GetCloudResourceGroups(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudResourceGroups(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives StorageAccount details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of StorageAccount data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraStorageAccount> GetCloudStorageAccount(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudStorageAccountList(requestInfo);
        }
    }
}
