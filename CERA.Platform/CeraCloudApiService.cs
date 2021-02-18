using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.DataOperation;
using CERA.Entities;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService azureServices;
        ICeraAwsApiService awsServices;
        ICeraDataOperation dataOps;

        public CeraCloudApiService()
        {

        }
        public object GetCloudMonthlyBillingList()
        {
            azureServices.GetHashCode();
            awsServices.GetHashCode();
            return new object();
        }

        public object GetCloudResourceList()
        {

            return new object();
        }

        public object GetCloudSqlDbList()
        {
            return new object();
        }

        public object GetCloudSqlServerList()
        {
            return new object();
        }

        public List<CeraSubscription> GetCloudSubscriptionList()
        {
            var subscriptions = azureServices.GetCloudSubscriptionList();
            dataOps.AddSubscriptionData(subscriptions);
            return subscriptions;
        }

        public object GetCloudServicePlanList()
        {
            return new object();
        }

        public object GetCloudTenantList()
        {
            return new object();
        }

        public List<CeraVM> GetCloudVMList()
        {
            var azvms = azureServices.GetCloudVMList();
            var awsvms = awsServices.GetCloudVMList();
            var allvms = new List<CeraVM>();
            allvms.AddRange(azvms);
            allvms.AddRange(awsvms);
            return allvms;
        }

        public object GetCloudWebAppList()
        {
            return new object();
        }

        public void GetAllResources()
        {
            GetCloudServicePlanList();
            GetCloudTenantList();
            GetCloudVMList();
            GetCloudWebAppList();
        }

        public List<CeraSubscription> GetSubscriptionList()
        {
            return dataOps.GetSubscriptions();
        }
    }
}
