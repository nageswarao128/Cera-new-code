using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public partial interface ICeraCloudApiService
    {
        public ICeraLogger Logger { get; set; }
        public void Initialize(string tenantId , string clientID , string clientSecret,string authority);
        public object GetCloudTenantList();
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo);
        public List<CeraVM> GetCloudVMList();
        public object GetCloudResourceList();
        public object GetCloudServicePlanList();
        public object GetCloudWebAppList();
        public object GetCloudSqlServerList();
        public object GetCloudSqlDbList();
        public object GetCloudMonthlyBillingList();
        public List<CeraSubscription> GetSubscriptionList();
    }
}
