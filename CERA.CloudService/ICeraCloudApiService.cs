using CERA.Entities;
using CERA.Entities.ViewModel;
using CERA.Logging;
using System;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public interface ICeraCloudApiService
    {
        public ICeraLogger Logger { get; set; }
        public void Initialize(string tenantId = "73d3d213-f87a-4465-9a7e-67bd625fdf9c", string clientID = "218411ec-50a7-4c7e-b671-e3434f3775d3", string clientSecret = "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU");
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
