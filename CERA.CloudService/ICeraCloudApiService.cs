using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERA.CloudService
{
    public partial interface ICeraCloudApiService
    {
        public ICeraLogger Logger { get; set; }
        public void Initialize(string tenantId, string clientID, string clientSecret,string authority);
        public List<CeraTenants> GetCloudTenantList(RequestInfoViewModel requestInfo);
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo);
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo);
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo,List<CeraSubscription> subscriptions);
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo);
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo);
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo);
        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo);
        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo);
        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo);

        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo,List<CeraSubscription> subscriptions);
        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo);
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo);
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo);
        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo);
        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo);
        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo);
        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo,List<CeraSubscription> subscriptions);
        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo);
        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public object GetCloudServicePlanList();
        public object GetCloudSqlDbList();
        public object GetCloudMonthlyBillingList();
        public List<CeraSubscription> GetSubscriptionList();
        public List<CeraResources> GetResourcesList();
        public List<CeraVM> GetVMList();
        public List<CeraResourceGroups> GetResourceGroupsList();
        public List<CeraStorageAccount> GetStorageAccountList();
        public List<CeraSqlServer> GetSqlServersList();
        public List<CeraTenants> GetTenantsList();
        public List<CeraWebApps> GetWebAppsList();
        public List<CeraAppServicePlans> GetAppServicePlansList();
        public List<CeraDisks> GetDisksList();
        public List<ResourceHealthViewDTO> GetCeraResourceHealthList();
        public List<CeraCompliances> GetCompliancesList();
        public List<CeraRateCard> GetRateCardList();
        public List<CeraUsage> GetUsageDetails();
        public List<CeraResourceTypeUsage> GetResourceTypeUsageDetails();
        public List<ResourceTypeCount> GetResourceTypeCounts();
        public List<CeraPolicy> GetPolicies();
        public List<ResourceTagsCount> GetResourceTagsCount();
        public List<AzureLocations> GetLocations();

    }
}
