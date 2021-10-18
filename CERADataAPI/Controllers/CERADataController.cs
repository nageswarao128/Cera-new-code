using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CERADataAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CERADataController : ControllerBase
    {
        private readonly ILogger<CERADataController> _logger;
        ICeraPlatform _ceraCloud;
        public CERADataController(ILogger<CERADataController> logger, ICeraPlatform ceraCloud)
        {
            _logger = logger;
            _ceraCloud = ceraCloud;

        }
        /// <summary>
        /// This method will retrives the Subscription data from the database
        /// </summary>
        /// <returns>returns Subscription data from database</returns>
        [HttpGet]
       
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
        /// <summary>
        /// This method will retrives the Resources data from the database
        /// </summary>
        /// <returns>returns Resources data from database</returns>
        [HttpGet]
        public IEnumerable<CeraResources> GetDBResources()
        {
            return _ceraCloud.GetResourcesList();
        }
        /// <summary>
        /// This method will retrives the Virtual Machines data from the database
        /// </summary>
        /// <returns>returns Virtual Machines data from database</returns>
        [HttpGet]
        public IEnumerable<CeraVM> GetDBVM()
        {
            return _ceraCloud.GetVMList();
        }
        /// <summary>
        /// This method will retrives the ResourceGroups data from the database
        /// </summary>
        /// <returns>returns ResourceGroups data from database</returns>
        [HttpGet]
        public IEnumerable<CeraResourceGroups> GetDBResourceGroups()
        {
            return _ceraCloud.GetResourceGroupsList();
        }
        /// <summary>
        /// This method will retrives the StorageAccount data from the database
        /// </summary>
        /// <returns>returns StorageAccount data from database</returns>
        [HttpGet]
        public IEnumerable<CeraStorageAccount> GetDBStorageAccount()
        {
            return _ceraCloud.GetStorageAccountList();
        }
        /// <summary>
        /// This method will retrives the SqlServer data from the database
        /// </summary>
        /// <returns>returns SqlServer data from database</returns>
        [HttpGet]
        public IEnumerable<CeraSqlServer> GetDBSqlServer()
        {
            return _ceraCloud.GetSqlServersList();
        }
        /// <summary>
        /// This method will retrives the Tenants data from the database
        /// </summary>
        /// <returns>returns Tenants data from database</returns>
        [HttpGet]
        public IEnumerable<CeraTenants> GetDBTenants()
        {
            return _ceraCloud.GetTenantsList();
        }
        /// <summary>
        /// This method will retrives the WebApps data from the database
        /// </summary>
        /// <returns>returns WebApps data from database</returns>
        [HttpGet]
        public IEnumerable<CeraWebApps> GetDBWebApps()
        {
            return _ceraCloud.GetWebAppsList();
        }
        /// <summary>
        /// This method will retrives the AppServicePlans data from the database
        /// </summary>
        /// <returns>returns AppServicePlans data from database</returns>
        [HttpGet]
        public IEnumerable<CeraAppServicePlans> GetDBAppServicePlans()
        {
            return _ceraCloud.GetAppServicePlansList();
        }
        /// <summary>
        /// This method will retrives the Disks data from the database
        /// </summary>
        /// <returns>returns Disks data from database</returns>
        [HttpGet]
        public IEnumerable<CeraDisks> GetDBDisks()
        {
            return _ceraCloud.GetDisksList();
        }
        [HttpGet]
        public IEnumerable<ResourceHealthViewDTO> GetDBResourceHealth()
        {
            return _ceraCloud.GetCeraResourceHealthList();
        }
        [HttpGet]
        public IEnumerable<CeraPolicy> GetDBPolicies()
        {
            return _ceraCloud.GetPolicies();
        }
        [HttpGet]
        public object GetResourceTypeUsageDetails()
        {
            return _ceraCloud.GetResourceTypeUsageDetails();
        }
        [HttpGet]
        public IEnumerable<ResourceTypeCount> GetResourceTypeCount()
        {
            return _ceraCloud.GetResourceTypeCounts();
        }
        [HttpGet]
        public IEnumerable<UserClouds> GetUserClouds()
        {
            return _ceraCloud.GetUserClouds();
        }
        [HttpGet]
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName = "Quadrant")
        {
            return _ceraCloud.GetClientCloudDetails(clientName);
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            return _ceraCloud.GetResourceTagsCount();
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider)
        {
            return _ceraCloud.GetResourceTagsCloudCount(cloudprovider);
        }
        [HttpGet]
        public IEnumerable<AzureLocations> GetLocations()
        {
            return _ceraCloud.GetLocations();
        }
        [HttpGet]
        public IEnumerable<ResourceLocations> GetResourceLocations()
        {
            return _ceraCloud.GetResourceLocations();
        }
        [HttpGet]
        public List<locationFilter> GetMapLocationsFilter()
        {
            return _ceraCloud.GetMapLocationsFilter();
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.GetResourceSubscriptionCloudTagsCount(cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.UsageSubscriptionByMonth(cloudprovider, subscriptionid);
        }

        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceUsageByLocation(string location)
        {
            return _ceraCloud.ResourceUsage(location);
        }
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location, string cloudprovider)
        {
            return _ceraCloud.ResourceCloudUsage(location, cloudprovider);
        }
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.ResourceSubscriptionCloudUsage(location, cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCounts(string location, string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.GetResourceSubscriptionCloudTagsCount(location, cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<ResourceTypeCount> GetResourceTypeCountByLocation(string location)
        {
            return _ceraCloud.GetResourceTypeCount(location);
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCountByLocation(string location)
        {
            return _ceraCloud.GetResourceTagsCount(location);
        }
        [HttpGet]
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location, string cloudprovider)
        {
            return _ceraCloud.GetResourceCloudTagsCount(location, cloudprovider);
        }
        [HttpGet]
        public List<ResourceLocations> GetResourceByLocations(string location)
        {
            return _ceraCloud.GetResourceLocations(location);
        }
        [HttpGet]
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider)
        {
            return _ceraCloud.GetResourceCloudCount(cloudprovider);
        }
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.ResourceSubscriptionCloudspentUsage(cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.GetSubscriptionLocationList(location, cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId, string cloudprovider)
        {
            return _ceraCloud.GetSubscriptionTypeList(subscriptionId, cloudprovider);
        }
        [HttpGet]
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location, string cloudprovider)
        {
            return _ceraCloud.GetResourceTypecloudCount(location, cloudprovider);
        }
        [HttpGet]
        public List<AdvisorRecommendations> GetAdvisorRecommendations()
        {
            return _ceraCloud.GetAdvisorRecommendations();
        }
        [HttpGet]
        public List<PolicyRules> GetPolicyRules()
        {
            return _ceraCloud.GetPolicyRules();
        }
        [HttpGet]
        public List<CostUsage> UsageByMonth()
        {
            return _ceraCloud.UsageByMonth();
        }
        [HttpGet]
        public List<CostUsage> UsageCloudByMonth(string cloudprovider)
        {
            return _ceraCloud.UsageCloudByMonth(cloudprovider);
        }
        [HttpGet]
        public List<CostUsage> UsageHistory()
        {
            return _ceraCloud.UsageHistory();
        }
        [HttpGet]
        public List<UsageHistoryByMonth> UsageHistoryByMonth()
        {
            return _ceraCloud.UsageHistoryByMonth();
        }
        [HttpGet]
        public List<UsageByLocation> GetUsageByLocation()
        {
            return _ceraCloud.GetUsageByLocation();
        }
        [HttpGet]
        public List<UsageByResourceGroup> GetUsageByResourceGroup()
        {
            return _ceraCloud.GetUsageByResourceGroup();
        }
        [HttpGet]
        public List<ManageOrg> ManageOrganization()
        {
            return _ceraCloud.ManageOrganization();

        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCount()
        {
            return _ceraCloud.GetDashboardCount();
        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountFilters(string location, string cloudprovider)
        {
            return _ceraCloud.GetDashboardCountFilters(location, cloudprovider);
        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountLocation(string location)
        {
            return _ceraCloud.GetDashboardCountLocation(location);
        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider)
        {
            return _ceraCloud.GetDashboardCountCloud(cloudprovider);
        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.GetDashboardSubscriptionCountFilters(cloudprovider, subscriptionid);
        }
        [HttpGet]
        public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid)
        {
            return _ceraCloud.GetDashboardSubscriptionLocationFilters(location, cloudprovider, subscriptionid);
        }
    }
}
