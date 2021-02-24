using CERA.Entities;
using CERA.Logging;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public interface ICeraCloudApiService
    {
        public ICeraLogger Logger { get; set; }
        void Initialize(string tenantId, string clientID, string clientSecret);
        public object GetCloudTenantList();
        public List<CeraSubscription> GetCloudSubscriptionList();
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
