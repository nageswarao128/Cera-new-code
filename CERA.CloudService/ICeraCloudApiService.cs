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
        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo);

        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo,List<CeraSubscription> subscriptions);
        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo);
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo);
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo);
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
        public List<CeraResourceHealth> GetCeraResourceHealthList();
    }
}
