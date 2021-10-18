
using CERA.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERA.DataOperation
{
    public partial interface ICeraDataOperation
    {
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName);
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform);
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails);
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin);
        public List<CeraResourceTypeUsage> ResourceUsage();
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location, string cloudprovider);
        public List<CeraResourceTypeUsage> ResourceUsage(string location);
        public List<ResourceTypeCount> GetResourceTypeCount();
        public List<ManageOrg> ManageOrganization();
        public List<ResourceTypeCount> GetResourceTypeCount(string location);
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider);
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid);
        public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId, string cloudprovider);
        public List<ResourceHealthViewDTO> ResourceHealth();
        public List<UserClouds> GetUserClouds();
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName);
        public List<ResourceTagsCount> GetResourceTagsCount();
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location, string cloudprovider);
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider);
        public List<ResourceTagsCount> GetResourceTagsCount(string location);
        public List<ResourceLocations> GetResourceLocations();
        public List<locationFilter> GetMapLocationsFilter();
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid);
  public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string location, string cloudprovider, string subscriptionid);
        public List<ResourceLocations> GetResourceLocations(string location);
        public List<CostUsage> UsageByMonth();
        public List<CostUsage> UsageCloudByMonth(string cloudprovider);
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location, string cloudprovider);
        public List<CostUsage> UsageHistory();
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid);
  public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid);
        public List<UsageHistoryByMonth> UsageHistoryByMonth();
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid);
        public List<UsageByLocation> GetUsageByLocation();
        public List<UsageByResourceGroup> GetUsageByResourceGroup();
        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid);
        public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid);
        public List<DashboardCountModel> GetDashboardCount();
        public List<DashboardCountModel> GetDashboardCountFilters(string location, string cloudprovider);
        public List<DashboardCountModel> GetDashboardCountLocation(string location);
        public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider);
    }
}
