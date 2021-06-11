using CERA.AuthenticationService;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERA.AWS.CloudService
{
    public class CeraAWSApiService : ICeraAwsApiService
    {
        public CeraAWSApiService()
        {

        }
        ICeraAuthenticator authenticator;

        public ICeraLogger Logger { get; set; }
        public List<CeraPlatformConfigViewModel> _platformConfigs { get; set; }

        public object GetCloudMonthlyBillingList()
        {
            return new object();
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraResources>();
        }

        public object GetCloudSqlDbList()
        {
            return new object();
        }

        public object GetCloudSqlServerList()
        {
            return new object();
        }

        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraSubscription>();
        }

        public object GetCloudServicePlanList()
        {
            return new object();
        }

        public List<CeraTenants> GetCloudTenantList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraTenants>();
        }

        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraVM>();
        }

        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraWebApps>();
        }
        public List<CeraSubscription> GetSubscriptionList()
        {
            return new List<CeraSubscription>();
        }
        public List<CeraResources> GetResourcesList()
        {
            return new List<CeraResources>();
        }
        public List<CeraVM> GetVMList()
        {
            return new List<CeraVM>();
        }
        public List<CeraResourceGroups> GetResourceGroupsList()
        {
            return new List<CeraResourceGroups>();
        }

        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            throw new NotImplementedException();
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraStorageAccount> GetStorageAccountList()
        {
            throw new NotImplementedException();
        }

        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraSqlServer> GetSqlServersList()
        {
            throw new NotImplementedException();
        }

        public List<CeraTenants> GetTenantsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraWebApps> GetWebAppsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetAppServicePlansList()
        {
            throw new NotImplementedException();
        }

        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraDisks> GetDisksList()
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<ResourceHealthViewDTO> GetCeraResourceHealthList()
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCompliancesList()
        {
            throw new NotImplementedException();
        }

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraRateCard> GetRateCardList()
        {
            throw new NotImplementedException();
        }

        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraUsage> GetUsageDetails()
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceTypeUsage> GetResourceTypeUsageDetails()
        {
            throw new NotImplementedException();
        }

        public List<ResourceTypeCount> GetResourceTypeCounts()
        {
            throw new NotImplementedException();
        }

        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraPolicy> GetPolicies()
        {
            throw new NotImplementedException();
        }

        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            throw new NotImplementedException();
        }

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<AzureLocations> GetLocations()
        {
            throw new NotImplementedException();
        }
    }
}
