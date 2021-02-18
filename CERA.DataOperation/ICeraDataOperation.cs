using CERA.Entities;
using System.Collections.Generic;

namespace CERA.DataOperation
{
    public interface ICeraDataOperation
    {
        public object AddTenantData(object data);
        public int AddSubscriptionData(List<CeraSubscription> data);
        public object AddResourceData(object data);
        public object AddWebAppData(object data);
        public object AddServicePlanData(object data);
        public object AddVMData(object data);
        public object AddSqlServerData(object data);
        public object AddSqlDbData(object data);
        public object UpdateTenantData(object data);
        public object UpdateSubscriptionData(object data);
        public object UpdateResourceData(object data);
        public object UpdateWebAppData(object data);
        public object UpdateServicePlanData(object data);
        public object UpdateVMData(object data);
        public object UpdateSqlServerData(object data);
        public object UpdateSqlDbData(object data);
        public List<CeraSubscription> GetSubscriptions();
    }
}
