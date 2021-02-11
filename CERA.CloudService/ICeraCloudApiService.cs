using CERA.CloudService.CERAEntities;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public interface ICeraCloudApiService
    {
        public object GetTenantsList();
        public object GetSubscriptionsList();
        public List<CeraVM> GetVMsList();
        public object GetResourcesList();
        public object GetSurvicePlansList();
        public object GetWebAppsList();
        public object GetSqlServersList();
        public object GetSqlDbsList();
        public object GetMonthlyBillingsList();
    }
}
