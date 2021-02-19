using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.DataOperation;
using CERA.Entities;
using CERA.Logging;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService _azureServices;
        ICeraAwsApiService _awsServices;
        ICeraDataOperation _dataOps;
        private ICeraLogger _logger;

        public CeraCloudApiService(ICeraAzureApiService azureServices, ICeraAwsApiService awsServices, ICeraDataOperation dataOps, ICeraLogger logger)
        {
            _azureServices = azureServices;
            _awsServices = awsServices;
            _dataOps = dataOps;
            _logger = logger;
        }
        public object GetCloudMonthlyBillingList()
        {
            _azureServices.GetHashCode();
            _awsServices.GetHashCode();
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
            _logger.LogInfo("Get Cloud Subcription List Called");
            var subscriptions = _azureServices.GetCloudSubscriptionList();
            _logger.LogInfo("Got data from Cloud Subcription");
            _dataOps.AddSubscriptionData(subscriptions);
            _logger.LogInfo("Imported data for Cloud Subcription to DB");
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
            var azvms = _azureServices.GetCloudVMList();
            var awsvms = _awsServices.GetCloudVMList();
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
            _logger.LogInfo("Requested data for Subcription List from Database called");
            return _dataOps.GetSubscriptions();
        }
    }
}
