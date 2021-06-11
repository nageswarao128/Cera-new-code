using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using System.Collections.Generic;

namespace CERA.DataOperation
{
    public partial interface ICeraDataOperation
    {
        public int AddTenantData(List<CeraTenants> data);
        public int AddSubscriptionData(List<CeraSubscription> data);
        public int AddResourcesData(List<CeraResources> data);
        public int AddResourceGroupData(List<CeraResourceGroups> data);
        public int AddStorageAccountData(List<CeraStorageAccount> data);
        public int AddSqlServerData(List<CeraSqlServer> data);
        public int AddWebAppData(List<CeraWebApps> data);
        public object AddServicePlanData(object data);
        public int AddVMData(List<CeraVM> data);
        public int AddAppServicePlans(List<CeraAppServicePlans> data);
        public int AddDisksData(List<CeraDisks> data);
        public int AddResourceHealth(List<CeraResourceHealth> data);
        public int AddCompliances(List<CeraCompliances> data);
        public int AddRateCard(List<CeraRateCard> data);
        public int AddUsageDetails(List<CeraUsage> data);
        public int AddLocationsData(List<AzureLocations> data);
        public int AddPolicyData(List<CeraPolicy> data);
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
        public List<CeraResourceGroups> GetResourceGroups();
        public List<CeraSqlServer> GetSqlServers();
        public List<CeraResourceHealth> GetResourceHealth();
        public List<CeraStorageAccount> GetStorageAccount();
        public List<CeraTenants> GetTenants();
        public List<CeraWebApps> GetWebApps();
        public List<CeraAppServicePlans> GetAppServicePlans();
        public List<CeraDisks> GetDisks();
        public List<CeraCompliances> GetCompliances();
        public List<CeraRateCard> GetRateCard();
        public List<CeraUsage> GetUsageDetails();
        public List<CeraPolicy> GetPolicy();
        public List<AzureLocations> GetLocations();
    }
}
