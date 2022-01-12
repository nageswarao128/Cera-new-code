using CERA.CloudService;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using System.Collections.Generic;

namespace CERA.Platform
{
    public interface ICeraPlatform : ICeraCloudApiService
    {
        public string ClientName { get; set; }
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName);
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform);
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails);
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin);
        public List<UserClouds> GetUserClouds();
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName);
        public string SyncCloudData(RequestInfoViewModel requestInfoViewModel);
        public List<ResourceLocations> GetResourceLocations();
        public List<ManageOrg> ManageOrganization();
        public List<locationFilter> GetMapLocationsFilter();
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid);
        public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid);
        public List<CeraResourceTypeUsage> ResourceUsage(string location);
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location, string cloudprovider);
        public List<ResourceTypeCount> GetResourceTypeCount(string location);
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider);
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid);
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location, string cloudprovider);
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid);
        public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId, string cloudprovider);
        public List<ResourceTagsCount> GetResourceTagsCount(string location);
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location, string cloudprovider);
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider);
        public List<ResourceLocations> GetResourceLocations(string location);
        public int AddPolicyRules(List<PolicyRules> data);
        public List<PolicyRules> GetPolicyRules();
        public List<CostUsage> UsageByMonth();
        public List<CostUsage> UsageCloudByMonth(string cloudprovider);
        public List<CostUsage> UsageHistory();
        public List<UsageHistoryByMonth> UsageHistoryByMonth();
        public List<UsageByLocation> GetUsageByLocation();
        public List<UsageByResourceGroup> GetUsageByResourceGroup();
        public List<DashboardCountModel> GetDashboardCount();
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid);
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string location, string cloudprovider, string subscriptionid);
        public List<DashboardCountModel> GetDashboardCountFilters(string location, string cloudprovider);
        public List<DashboardCountModel> GetDashboardCountLocation(string location);
        public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider);
        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid);
        public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid);
        public List<Dashboardresources> GetComputeResources();
        public List<Dashboardresources> GetNetworkResources();
        public List<Dashboardresources> GetStorageResources();
        public List<Dashboardresources> GetOtherResources();
        public List<ResourcesModel> GetResourceGroupResources(string name);
        public List<BarChartModel> GetBarChartCloudData(string cloud);
        public List<BarChartModel> GetBarChartSubscriptionData(string cloud, string subscriptionId);
        public List<StorageSize> GetStorageSizes();
    }
}
