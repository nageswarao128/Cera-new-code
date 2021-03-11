using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using System.Collections.Generic;

namespace CERA.DataOperation
{
    public partial interface ICeraDataOperation
    {
        public object AddTenantData(object data);
        public int AddSubscriptionData(List<CeraSubscription> data);
        public int AddResourcesData(List<CeraResources> data);   
        
        public object AddWebAppData(object data);
        public object AddServicePlanData(object data);
        public int AddVMData(List<CeraVM> data);
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
        public List<CeraResources> GetResources();
        public List<CeraVM> GetVM();


    }
}
