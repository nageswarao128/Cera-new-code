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

        private readonly ILogger<CeraController> logger;
        ICeraPlatform ceraCloud;
        public CeraController(ILogger<CeraController> _logger, ICeraPlatform _ceraCloud)
        {
            logger = _logger;
            ceraCloud = _ceraCloud;
        }

        /// <summary>
        /// To sync cloud data
        /// </summary>
        /// <param name="requestInfoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object SyncCloudData(RequestInfoViewModel requestInfoViewModel)
        {
            string ClientName = "Quadrant";
            
            ceraCloud.ClientName = ClientName;
            return ceraCloud.SyncCloudData(requestInfoViewModel);
        }

        /// <summary>
        /// To get available locations for the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AzureLocations> GetCloudLocations(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudLocations(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudSubscriptionList(requestInfo);
        }

        /// <summary>
        /// to get resources health from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraResourceHealth> GetCloudResourceHealth(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudResourceHealth(requestInfo);
        }

        /// <summary>
        /// to get rate card of the card
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraRateCard> GetCloudRateCard(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudRateCardList(requestInfo);
        }

        /// <summary>
        /// to get usage details of cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CeraUsage>> GetCloudUsageDetails(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return await ceraCloud.GetCloudUsageDetails(requestInfo);
        }

        /// <summary>
        /// to get cloud complainces
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraCompliances> GetCloudCompliances(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudCompliances(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudResourceList(requestInfo);
        }

        /// <summary>
        /// Based on the cloud this method will retrives Virtual Machines details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of Virtual Machines data from cloud</returns>
        [HttpGet]
        public async Task<IEnumerable<CeraVM>> GetCloudVM(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return await ceraCloud.GetCloudVMList(requestInfo);
        }

        /// <summary>
        /// Based on the cloud this method will retrives ResourceGroups details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of ResourceGroups data from cloud</returns>
        [HttpGet]
        public async Task<IEnumerable<CeraResourceGroups>> GetCloudResourceGroups(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return await ceraCloud.GetCloudResourceGroups(requestInfo);
        }

        /// <summary>
        /// Based on the cloud this method will retrives StorageAccount details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of StorageAccount data from cloud</returns>
        [HttpGet]
        public async Task<IEnumerable<CeraStorageAccount>> GetCloudStorageAccount(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return await ceraCloud.GetCloudStorageAccountList(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudSqlServersList(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudTenantList(requestInfo);
        }

        /// <summary>
        /// Based on the cloud this method will retrives WebApps details from the cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns the list of WebApps data from cloud</returns>
        [HttpGet]
        public async Task<IEnumerable<CeraWebApps>> GetCloudWebApps(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return await ceraCloud.GetCloudWebAppList(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudAppServicePlansList(requestInfo);
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
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudDisksList(requestInfo);
        }

        /// <summary>
        /// to get policies from cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CeraPolicy> GetCloudPolicies(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudPolicies(requestInfo);
        }

        /// <summary>
        /// to get advisor recommendations from cloud
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AdvisorRecommendations> GetCloudAdvisorRecommendations(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudAdvisorRecommendations(requestInfo);
        }

        /// <summary>
        /// to get cloud usage details for a month
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UsageByMonth> GetCloudUsageByMonth(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudUsageByMonth(requestInfo);
        }

        /// <summary>
        /// to get cloud usage details for past 6 months
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UsageHistory> GetCloudUsageHistory(string ClientName = "Quadrant")
        {
            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            ceraCloud.ClientName = ClientName;
            return ceraCloud.GetCloudUsageHistory(requestInfo);
        }

        /// <summary>
        /// adds application defined policies
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public object AddPolicyRules(List<PolicyRules> data)
        {
            return ceraCloud.AddPolicyRules(data);
        }
    }
}
