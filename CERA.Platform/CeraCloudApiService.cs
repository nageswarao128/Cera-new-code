using CERA.Azure.CloudService;
using CERA.Converter;
using CERA.DataOperation;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using CERA.Logging;
using System.Collections.Generic;
using System.Reflection;
using CERA.DataOperation;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using CERA.DataOperation.CeraContext;
using Microsoft.EntityFrameworkCore;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService _azureServices;
        ICeraAwsApiService _awsServices;
        ICeraDataOperation _dataOps;
        ICeraConverter _converter;
        private ICeraLogger _logger;
        ICeraAzureApiService azureServices;
        // ICeraAwsApiService awsServices;
        //ICeraDataOperation dataOperation;

        List<CeraPlatformConfig> _platformConfigs = new List<CeraPlatformConfig>() {
            new CeraPlatformConfig(){PlatformName =   "Azure", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "Aws", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "GCP", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "IBM", APIClassName = "", DllPath = ""},
        };
       private CeraDbContext _DbContext;
        public CeraCloudApiService()
        {

        public CeraCloudApiService(ICeraAzureApiService azureServices,
            ICeraAwsApiService awsServices,
            ICeraDataOperation dataOps,
            ICeraLogger logger,
            ICeraConverter converter)
        {
            _azureServices = azureServices;
            _awsServices = awsServices;
            _dataOps = dataOps;
            _logger = logger;
            _converter = converter;
        }
        public object GetCloudMonthlyBillingList()
        {
            _azureServices.GetHashCode();
            _awsServices.GetHashCode();
            azureServices.GetHashCode();
           // awsServices.GetHashCode();
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
            ///Get List of Cloud services for the client
            ///usig Reflection instantiate cloud service for corresponding Cloud Platform using itteration
            ///ICeraCloudApiService i = using Reflection create instance of cloud service
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            foreach (var platformConfig in _platformConfigs)
            {
                ICeraCloudApiService _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                subscriptions = _cloudApiServices.GetCloudSubscriptionList();
                _logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Subcription");
                _dataOps.AddSubscriptionData(subscriptions);
                subscriptions.Clear();
                _logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Subcription to DB");
            }
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
            var azvms = azureServices.GetCloudVMList();
            //var awsvms = awsServices.GetCloudVMList();
            var allvms = new List<CeraVM>();
            allvms.AddRange(azvms);
           // allvms.AddRange(awsvms);
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

        public void Initialize(string tenantId, string clientID, string clientSecret)
        {
            _azureServices.Initialize(tenantId, clientID, clientSecret);
        }
        
        public List<CeraSubscriptionList> GetSubscriptionsList(string authority, string clientId, string clientSecret, string redirectUrl, string tenantId)
        {
            var subscriptions = azureServices.GetSubscriptionsList(authority, clientId, clientSecret, redirectUrl, tenantId);
           
            // CERADataOperation data = new CERADataOperation();
            // var sample = data.AddSubscriptionData(list);
            throw new System.NotImplementedException();
        }
    }
}
