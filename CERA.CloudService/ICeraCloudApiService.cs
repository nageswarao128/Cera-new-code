using CERA.Entities;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public interface ICeraCloudApiService
    {
        public object GetCloudTenantList();
        public object GetSubscriptionsList();
        public List<CeraVM> GetCloudVMList();
        public object GetResourcesList();
        public object GetSurvicePlansList();
        public object GetWebAppsList();
        public object GetSqlServersList();
        public object GetSqlDbsList();
        public object GetMonthlyBillingsList();
    }
}
