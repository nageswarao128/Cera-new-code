using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERASyncAPI.Controllers
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
        [HttpPost]
        public object SyncCloudData(RequestInfoViewModel requestInfoViewModel)
        {
            string ClientName = "Quadrant";
            //RequestInfoViewModel requestInfoViewModel = new RequestInfoViewModel();
            //requestInfoViewModel.token = token;
            //requestInfoViewModel.tenantId = tenantid;
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.SyncCloudData(requestInfoViewModel);
        }
        [HttpGet]
        public IEnumerable<AzureLocations> GetCloudLocations(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudLocations(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives Subscription details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Subscription data from cloud</returns>  
        [HttpGet]
        public IEnumerable<CeraSubscription> GetCloudSubscriptions(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudSubscriptionList(requestInfo);
        }
        [HttpGet]
        public List<CeraResourceHealth> GetCloudResourceHealth(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudResourceHealth(requestInfo);
        }
        [HttpGet]
        public List<CeraRateCard> GetCloudRateCard(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudRateCardList(requestInfo);
        }
        [HttpGet]
        public List<CeraUsage> GetCloudUsageDetails(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudUsageDetails(requestInfo);
        }
        [HttpGet]
        public List<CeraCompliances> GetCloudCompliances(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudCompliances(requestInfo);
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
        /// <summary>
        /// Based on the cloud this method will retrives SqlServer details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of SqlServer data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraSqlServer> GetCloudSqlServer(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudSqlServersList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives Tenants details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Tenants data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraTenants> GetCloudTenants(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudTenantList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives WebApps details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of WebApps data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraWebApps> GetCloudWebApps(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudWebAppList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives AppServicePlans details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of AppServicePlans data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraAppServicePlans> GetCloudAppServicePlans(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudAppServicePlansList(requestInfo);
        }
        /// <summary>
        /// Based on the cloud this method will retrives Disks details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Disks data from cloud</returns>
        [HttpGet]
        public IEnumerable<CeraDisks> GetCloudDisks(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudDisksList(requestInfo);
        }
        [HttpGet]
        public IEnumerable<CeraPolicy> GetCloudPolicies(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            _ceraCloud.ClientName = ClientName;
            return _ceraCloud.GetCloudPolicies(requestInfo);
        }
        
    }
}
