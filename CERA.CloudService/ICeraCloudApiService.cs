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
        public void Initialize(string tenantId /*= "73d3d213-f87a-4465-9a7e-67bd625fdf9c"*/, string clientID /*= "218411ec-50a7-4c7e-b671-e3434f3775d3"*/, string clientSecret /*= "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU"*/,string authority);
        public object GetCloudTenantList();
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo);
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo);
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo,List<CeraSubscription> subscriptions);
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo);
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);

        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo);
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo);
        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions);
        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo);

        public object GetCloudServicePlanList();
        public object GetCloudWebAppList();
        public object GetCloudSqlServerList();
        public object GetCloudSqlDbList();
        public object GetCloudMonthlyBillingList();
        public List<CeraSubscription> GetSubscriptionList();
        public List<CeraResources> GetResourcesList();
        public List<CeraVM> GetVMList();
        public List<CeraResourceGroups> GetResourceGroupsList();
        public List<CeraStorageAccount> GetStorageAccountList();
        public List<CeraResourceHealth> GetCeraResourceHealthList();
    }
}
