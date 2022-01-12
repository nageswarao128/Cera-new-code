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
        private readonly ILogger<CERADataController> logger;
        ICeraPlatform ceraCloud;
        public CERADataController(ILogger<CERADataController> _logger, ICeraPlatform _ceraCloud)
        {
            logger = _logger;
            ceraCloud = _ceraCloud;

        }

        /// <summary>
        /// This method will retrives the Subscription data from the database
        /// </summary>
        /// <returns>returns Subscription data from database</returns>
        [HttpGet]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return ceraCloud.GetSubscriptionList();
        }

        /// <summary>
        /// This method will retrives the Resources data from the database
        /// </summary>
        /// <returns>returns Resources data from database</returns>
        [HttpGet]
        public IEnumerable<CeraResources> GetDBResources()
        {
            return ceraCloud.GetResourcesList();
        }

        /// <summary>
        /// This method will retrives the Virtual Machines data from the database
        /// </summary>
        /// <returns>returns Virtual Machines data from database</returns>
        [HttpGet]
        public IEnumerable<ResourcesModel> GetDBVM()
        {
            return ceraCloud.GetVMList();
        }

        /// <summary>
        /// This method will retrives the ResourceGroups data from the database
        /// </summary>
        /// <returns>returns ResourceGroups data from database</returns>
        [HttpGet]
        public IEnumerable<ResourceGroupsVM> GetDBResourceGroups()
        {
            return ceraCloud.GetResourceGroupsList();
        }

        /// <summary>
        /// This method will retrives the StorageAccount data from the database
        /// </summary>
        /// <returns>returns StorageAccount data from database</returns>
        [HttpGet]
        public IEnumerable<StorageAccountsVM> GetDBStorageAccount()
        {
            return ceraCloud.GetStorageAccountList();
        }

        [HttpGet]
        public List<StorageSize> GetStorageSizes()
        {
            return ceraCloud.GetStorageSizes();
        }

        /// <summary>
        /// This method will retrives the SqlServer data from the database
        /// </summary>
        /// <returns>returns SqlServer data from database</returns>
        [HttpGet]
        public IEnumerable<CeraSqlServer> GetDBSqlServer()
        {
            return ceraCloud.GetSqlServersList();
        }

        /// <summary>
        /// This method will retrives the Tenants data from the database
        /// </summary>
        /// <returns>returns Tenants data from database</returns>
        [HttpGet]
        public IEnumerable<CeraTenants> GetDBTenants()
        {
            return ceraCloud.GetTenantsList();
        }

        /// <summary>
        /// This method will retrives the WebApps data from the database
        /// </summary>
        /// <returns>returns WebApps data from database</returns>
        [HttpGet]
        public IEnumerable<ResourcesModel> GetDBWebApps()
        {
            return ceraCloud.GetWebAppsList();
        }

        /// <summary>
        /// This method will retrives the AppServicePlans data from the database
        /// </summary>
        /// <returns>returns AppServicePlans data from database</returns>
        [HttpGet]
        public IEnumerable<CeraAppServicePlans> GetDBAppServicePlans()
        {
            return ceraCloud.GetAppServicePlansList();
        }

        /// <summary>
        /// This method will retrives the Disks data from the database
        /// </summary>
        /// <returns>returns Disks data from database</returns>
        [HttpGet]
        public IEnumerable<CeraDisks> GetDBDisks()
        {
            return ceraCloud.GetDisksList();
        }

        /// <summary>
        /// This method will retrives the Resources Health data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ResourceHealthViewDTO> GetDBResourceHealth()
        {
            return ceraCloud.GetCeraResourceHealthList();
        }

        /// <summary>
        /// This method will retrives the Policies data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CeraPolicy> GetDBPolicies()
        {
            return ceraCloud.GetPolicies();
        }

        /// <summary>
        /// This method will retrives the Usage details by resouce type data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetResourceTypeUsageDetails()
        {
            return ceraCloud.GetResourceTypeUsageDetails();
        }

        /// <summary>
        /// This method will retrives the count of resources for each resource type data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ResourceTypeCount> GetResourceTypeCount()
        {
            return ceraCloud.GetResourceTypeCounts();
        }

        /// <summary>
        /// This method will retrives the available clouds data for the user data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserClouds> GetUserClouds()
        {
            return ceraCloud.GetUserClouds();
        }

        /// <summary>
        /// This method will retrives the cloud details of the client data from the database
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName = "Quadrant")
        {
            return ceraCloud.GetClientCloudDetails(clientName);
        }

        /// <summary>
        /// This method will retrives the tags count for the resources data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            return ceraCloud.GetResourceTagsCount();
        }

        /// <summary>
        /// This method will retrives the resources tags count for a given cloud data from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider)
        {
            return ceraCloud.GetResourceTagsCloudCount(cloudprovider);
        }

        /// <summary>
        /// This method will retrives all locations data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AzureLocations> GetLocations()
        {
            return ceraCloud.GetLocations();
        }

        /// <summary>
        /// This method will retrives the resources location data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ResourceLocations> GetResourceLocations()
        {
            return ceraCloud.GetResourceLocations();
        }

        /// <summary>
        /// This method will retrives the map data by location from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<locationFilter> GetMapLocationsFilter()
        {
            return ceraCloud.GetMapLocationsFilter();
        }

        /// <summary>
        /// This method will retrives the resources data by cloud and subscription data from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid)
        {
            return ceraCloud.GetResourceSubscriptionCloudTagsCount(cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the usage data by cloud and subscription from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid)
        {
            return ceraCloud.UsageSubscriptionByMonth(cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the resources usage data by location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceUsageByLocation(string location)
        {
            return ceraCloud.ResourceUsage(location);
        }

        /// <summary>
        /// This method will retrives the resources usage data by cloud and subscription from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location, string cloudprovider)
        {
            return ceraCloud.ResourceCloudUsage(location, cloudprovider);
        }

        /// <summary>
        /// This method will retrives the resources data by cloud , subscription and location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid)
        {
            return ceraCloud.ResourceSubscriptionCloudUsage(location, cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the resources tags data by cloud,subscription and location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCounts(string location, string cloudprovider, string subscriptionid)
        {
            return ceraCloud.GetResourceSubscriptionCloudTagsCount(location, cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the resources count data by location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTypeCount> GetResourceTypeCountByLocation(string location)
        {
            return ceraCloud.GetResourceTypeCount(location);
        }

        /// <summary>
        /// This method will retrives the resources tags count data by location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceTagsCountByLocation(string location)
        {
            return ceraCloud.GetResourceTagsCount(location);
        }

        /// <summary>
        /// This method will retrives the resources tags count data by location and cloud from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location, string cloudprovider)
        {
            return ceraCloud.GetResourceCloudTagsCount(location, cloudprovider);
        }

        /// <summary>
        /// This method will retrives the resources data by location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceLocations> GetResourceByLocations(string location)
        {
            return ceraCloud.GetResourceLocations(location);
        }

        /// <summary>
        /// This method will retrives the resources count data by cloud from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider)
        {
            return ceraCloud.GetResourceCloudCount(cloudprovider);
        }

        /// <summary>
        /// This method will retrives the resources usage data by cloud and subscription from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid)
        {
            return ceraCloud.ResourceSubscriptionCloudspentUsage(cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the resources location data by location,cloud and subscription from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid)
        {
            return ceraCloud.GetSubscriptionLocationList(location, cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the resources count data by cloud and subscription from the database
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId, string cloudprovider)
        {
            return ceraCloud.GetSubscriptionTypeList(subscriptionId, cloudprovider);
        }

        /// <summary>
        /// This method will retrives the resources count data by cloud and location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location, string cloudprovider)
        {
            return ceraCloud.GetResourceTypecloudCount(location, cloudprovider);
        }

        /// <summary>
        /// This method will retrives the AdvisorRecommendations data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<AdvisorRecommendations> GetAdvisorRecommendations()
        {
            return ceraCloud.GetAdvisorRecommendations();
        }

        /// <summary>
        /// This method will retrives the Policiy rules data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<PolicyRules> GetPolicyRules()
        {
            return ceraCloud.GetPolicyRules();
        }

        /// <summary>
        /// This method will retrives the usage data for a month from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CostUsage> UsageByMonth()
        {
            return ceraCloud.UsageByMonth();
        }

        /// <summary>
        /// This method will retrives the usage data by cloud from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<CostUsage> UsageCloudByMonth(string cloudprovider)
        {
            return ceraCloud.UsageCloudByMonth(cloudprovider);
        }

        /// <summary>
        /// This method will retrives the usage data for six months from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CostUsage> UsageHistory()
        {
            return ceraCloud.UsageHistory();
        }

        /// <summary>
        /// This method will retrives the usage data for six months from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UsageHistoryByMonth> UsageHistoryByMonth()
        {
            return ceraCloud.UsageHistoryByMonth();
        }

        /// <summary>
        /// This method will retrives the usage data by location from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UsageByLocation> GetUsageByLocation()
        {
            return ceraCloud.GetUsageByLocation();
        }

        /// <summary>
        /// This method will retrives the usage data by resource group from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UsageByResourceGroup> GetUsageByResourceGroup()
        {
            return ceraCloud.GetUsageByResourceGroup();
        }

        /// <summary>
        /// This method will retrives the list of clients from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ManageOrg> ManageOrganization()
        {
            return ceraCloud.ManageOrganization();

        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCount()
        {
            return ceraCloud.GetDashboardCount();
        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard by location and cloud from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountFilters(string location, string cloudprovider)
        {
            return ceraCloud.GetDashboardCountFilters(location, cloudprovider);
        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard by location from the database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountLocation(string location)
        {
            return ceraCloud.GetDashboardCountLocation(location);
        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard by cloud from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider)
        {
            return ceraCloud.GetDashboardCountCloud(cloudprovider);
        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard by cloud and subscription from the database
        /// </summary>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid)
        {
            return ceraCloud.GetDashboardSubscriptionCountFilters(cloudprovider, subscriptionid);
        }

        /// <summary>
        /// This method will retrives the information for the tiles on the dashboard by location,cloud and subscription from the database
        /// </summary>
        /// <param name="location"></param>
        /// <param name="cloudprovider"></param>
        /// <param name="subscriptionid"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid)
        {
            return ceraCloud.GetDashboardSubscriptionLocationFilters(location, cloudprovider, subscriptionid);
        }

        [HttpGet]
        public List<Dashboardresources> GetComputeResources()
        {
            return ceraCloud.GetComputeResources();
        }

        [HttpGet]
        public List<Dashboardresources> GetNetworkResources()
        {
            return ceraCloud.GetNetworkResources();
        }

        [HttpGet]
        public List<Dashboardresources> GetStorageResources()
        {
            return ceraCloud.GetStorageResources();
        }

        [HttpGet]
        public List<Dashboardresources> GetOtherResources()
        {
            return ceraCloud.GetOtherResources();
        }

        [HttpGet]
        public List<ResourcesModel> GetResourceGroupResources(string name)
        {
            return ceraCloud.GetResourceGroupResources(name);
        }

        [HttpGet]
        public List<BarChartModel> GetBarChartCloudData(string cloud)
        {
            return ceraCloud.GetBarChartCloudData(cloud);
        }

        [HttpGet]
        public List<BarChartModel> GetBarChartSubscriptionData(string cloud, string subscriptionId)
        {
            return ceraCloud.GetBarChartSubscriptionData(cloud, subscriptionId);
        }
    }
}
